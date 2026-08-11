using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.enums;
using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class ApplicationService:IApplicationService
    {
        private readonly IUnitOfWork _unitofWork;
        private readonly IAttachService _attachService;
        private readonly IEmailSender _emailSender;

        public ApplicationService(IUnitOfWork unitofWork, IAttachService attachService, IEmailSender emailSender)
        {
            _unitofWork = unitofWork;
            _attachService = attachService;
            _emailSender = emailSender;
        }
        public async Task<List<ApplicationDto>> GetAppsByJObAdId(ApplicationJobAdCommand command)
        {
            var company = await _unitofWork.JobAdsRepo.GetCompanyWithJobAdIdAsync(command.JobAdId);
            if (company == null)
            {
                throw new NotFoundException("your company not found", "company-404");
            }
            if (!company.IsApproved)
            {
                throw new PermisionException("your company is not approved...", "company-403");
            }
            if (!await IsAdmin(command.RequesterId))
            {
                if (company.UserId != command.RequesterId)
                {
                    throw new PermisionException("this jobAd Does not belong to you", "jobAd-403");
                }
            }
            var jobad = await _unitofWork.JobAdsRepo.GetByIdAsync(command.JobAdId);
            if (jobad == null)
            {
                throw new NotFoundException("jobAd not found", "jobad-404");
            }
            var apps =await _unitofWork.JobAdsRepo.GetApplicationsJobAd(command.JobAdId);
            if (apps is null)
            {
                return new List<ApplicationDto>();
            }
            return apps.Select(a => new ApplicationDto
            {
                 ApplicationId = a.Id,
                 FirstName= a.User.FirstName,
                 LastName = a.User.LastName,
                 SubmitedAt = a.CreatedAt,
            }).OrderByDescending(a => a.SubmitedAt).ToList();

        }
        public async Task<DetailAppDto> GetDetailApp(AppDetailCommand command)
        {
            var app =await _unitofWork.ApplicationRepo.GetDetailAppbyIdAsync(command.AppId);
            if(app is null)
            {
                throw new NotFoundException("Application not found", "Application-404");
            }
            var company = await _unitofWork.JobAdsRepo.GetCompanyWithJobAdIdAsync(app.JobAdId);
            if (company == null)
            {
                throw new NotFoundException("your company not found", "company-404");
            }
            if (!company.IsApproved)
            {
                throw new PermisionException("your company is not approved...", "company-403");
            }
            if (!await IsAdmin(command.RequesterId))
            {
                if (company.UserId != command.RequesterId)
                {
                    throw new PermisionException("this jobAd Does not belong to you", "jobAd-403");
                }
            }
            app.ReviewedAt = DateTime.UtcNow;
            await _unitofWork.SaveChangesAsync();
            return new DetailAppDto { 
             AppId = app.Id,
             Email = app.User.Email,
             FirstName = app.User.FirstName,
             LastName = app.User.LastName,
             PhoneNumber = app.User.PhoneNumber,
             status = app.Status.ToString(),
             SubmitedAt = app.CreatedAt,
             Note = app.NoteWritenByUser,
             ResumeId = app.User.ResumeId,
            };
        }
        public async Task<string> ChangeApplicationStatusAsync(ChangeAppStatusCommand command)
        {
            var app = await _unitofWork.ApplicationRepo.GetByIdAsync(command.AppId,true);
            if (app is null)
            {
                throw new NotFoundException("Application not found", "Application-404");
            }
            var company = await _unitofWork.JobAdsRepo.GetCompanyWithJobAdIdAsync(app.JobAdId);
            if (company == null)
            {
                throw new NotFoundException("your company not found", "company-404");
            }
            if (!company.IsApproved)
            {
                throw new PermisionException("your company is not approved...", "company-403");
            }
            var jobad =await _unitofWork.JobAdsRepo.GetByIdAsync(app.JobAdId);
            if (jobad == null)
            {
                throw new NotFoundException("jobad not found", "jobad-404");
            }
            if (jobad.Status != JobAdStatus.Published)
            {
                throw new BadRequestException("JobAd is not Active..you cant Change it");
            }
            if (!await IsAdmin(command.RequesterId))
            {
                if (company.UserId != command.RequesterId)
                {
                    throw new PermisionException("this jobAd Does not belong to you", "jobAd-403");
                }
            }
            if (!Enum.TryParse<ApplicationStatus>(command.status,true,out var newstatus))
            {
                throw new BadRequestException("the status is not valid");
            }

            ValidateStatusTransition(app.Status, newstatus);

            app.Status = newstatus;
            app.ModifiedAt = DateTime.UtcNow;

            await _unitofWork.ApplicationRepo.SaveChangesAsync();
            string message = "";
            switch (newstatus)
            {
                case ApplicationStatus.Accepted:
                    message = $"your application was accepted for position {jobad.Title} in {company.Name} company ";
                    break;
                case ApplicationStatus.Rejected:
                    message = $"your application was rejected for position {jobad.Title} in {company.Name} company ";
                    break;
                case ApplicationStatus.Interview:
                    message = $"you was invited for position {jobad.Title} in {company.Name} company ";
                    break;
                case ApplicationStatus.InReview:
                    message = $"your application is reviewing for position {jobad.Title} in {company.Name} company ";
                    break;
            }
            await _emailSender.SendAsync(app.UserId,"Change Application status",message);
            return "succesfully Changed";
        }

        private void ValidateStatusTransition(ApplicationStatus currentStatus,ApplicationStatus targetStatus)
        {
            var allowed = currentStatus switch
            {
                ApplicationStatus.Submitted => targetStatus == ApplicationStatus.InReview,
                ApplicationStatus.InReview => targetStatus == ApplicationStatus.Interview || targetStatus == ApplicationStatus.Rejected,
                ApplicationStatus.Interview => targetStatus == ApplicationStatus.Accepted || targetStatus == ApplicationStatus.Rejected,
                _ => false
            };

            if (!allowed)
                throw new BadRequestException($"Invalid status transition from {currentStatus} to {targetStatus}.");
        }
        private async Task<bool> IsAdmin(Guid requesterid)
        {
            var user = await _unitofWork.userManager.FindByIdAsync(requesterid.ToString());
            return user != null && await _unitofWork.userManager.IsInRoleAsync(user, "Admin");
        }

        public async Task<string> CreateApplicationAsync(CreateAppCommand cmd)
        {
            if (!await IsAdmin(cmd.RequesterId))
            {
                if (cmd.UserId != cmd.RequesterId)
                {
                    throw new PermisionException("This user does not belong to you.", "UserApplication-403");
                }
            }
            var jobad = await _unitofWork.JobAdsRepo.GetJobAdWithCompanyAsync(cmd.jobAdID);
            if (jobad == null)
            {
                throw new NotFoundException("JobAd not found.", "JobAd-404");
            }

            if (jobad.Status != JobAdStatus.Published)
            {
                throw new PermisionException("JobAd is not active.", "JobAd-403");
            }

          


            if (await _unitofWork.ApplicationRepo.AlreadyExsist(cmd.UserId,cmd.jobAdID))
            {
                throw new BadRequestException("You have already applied for this job.");
            }

            var app = new Application(ApplicationStatus.Submitted, cmd.jobAdID, cmd.UserId, cmd.Note);

            await _unitofWork.ApplicationRepo.AddAsync(app);
            await _unitofWork.SaveChangesAsync();
            await _emailSender.SendAsync(jobad.Company.UserId,"New Application",$"you Have a New Application for Position {jobad.Title}");
            return "succesfully Created";
        }

        public async Task<List<AppDtoForCustomer>> GetAppsForJobSeekerAsync(Guid UserId,Guid RequesterId)
        {
            if (!await IsAdmin(RequesterId))
            {
                if (UserId != RequesterId)
                {
                    throw new PermisionException("This user does not belong to you.", "UserApplication-403");
                }
            }

            var apps =await _unitofWork.ApplicationRepo.GetJoinedApps(UserId);
            return apps.Select(a => new AppDtoForCustomer(a.Id,a.JObAd.Company.Name,a.JObAd.Title,a.JobAdId)).ToList();
        }
        public async Task<AppDetailForJobSeeker> GetAppDetailForJobSeekerAsync(Guid RequesterId,Guid appid)
        {
            var app = await _unitofWork.ApplicationRepo.GetDetailAppbyIdAsync(appid);
            if(app == null)
            {
                throw new NotFoundException("Application not found","Application-404");
            }
            if (!await IsAdmin(RequesterId))
            {
                if (app.UserId != RequesterId)
                {
                    throw new PermisionException("This user does not belong to you.", "UserApplication-403");
                }
            }
            return new AppDetailForJobSeeker(app.JobAdId,app.Id,app.Status.ToString(),app.ReviewedAt,app.CreatedAt,app.NoteWritenByUser,app.User.ResumeId);
        }
        public async Task<string> CancellMyApp(Guid requesterId,Guid appId)
        {

            var app = await _unitofWork.ApplicationRepo.GetDetailAppbyIdAsync(appId);
            if (app == null)
            {
                throw new NotFoundException("Application not found", "Application-404");
            }
            if (!await IsAdmin(requesterId))
            {
                if (app.UserId != requesterId)
                {
                    throw new PermisionException("This App does not belong to you.", "UserApplication-403");
                }
            }
            if(app.Status != ApplicationStatus.Submitted)
            {
                throw new BadRequestException("the Application has been seen by Company and you cant edit it");
            }
            app.Status = ApplicationStatus.canceled;
            await _unitofWork.SaveChangesAsync();
            return "succesfully Canceled";



        }
        public async Task<AttachOutputDto> GetResumeAsync(Guid requesterId,Guid appId)
        {
            var app =await _unitofWork.ApplicationRepo.GetJoinedAppByAppId(appId);
            if(app is null)
            {
                throw new NotFoundException("Application was not found","application_404");
            }
            if(! await IsAdmin(requesterId))
            {
                if(app.UserId != requesterId && app.JObAd.Company.UserId != requesterId)
                {
                    throw new PermisionException("this Application Resume does not blong to you and you cant see that","resume-403");
                }
            }
            if(app.User.ResumeId is null)
            {
                throw new NotFoundException("resume not found...the user has deleted her/his resume","Resume-404");
            }
            return await _attachService.DownloadAsync(app.User.ResumeId.Value);
        }


    }
}
