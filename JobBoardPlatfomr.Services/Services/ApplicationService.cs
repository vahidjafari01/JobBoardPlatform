using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
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

        public ApplicationService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
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
            return new DetailAppDto { 
             AppId = app.Id,
             Email = app.User.Email,
             FirstName = app.User.FirstName,
             LastName = app.User.LastName,
             PhoneNumber = app.User.PhoneNumber,
             status = app.Status.ToString(),
             SubmitedAt = app.CreatedAt,
             Note = app.NoteWritenByUser,
            };
        }
        public async Task ChangeApplicationStatusAsync(ChangeAppStatusCommand command)
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
            return await _unitofWork.userManager.IsInRoleAsync(user, "Admin");
        }

    }
}
