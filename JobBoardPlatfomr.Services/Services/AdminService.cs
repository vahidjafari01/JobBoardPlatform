using JobBoardPlatfomr.Domain.Abstractions;
using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.enums;
using JobBoardPlatform.Domain.Users;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class AdminService:IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICompanyService _companyService;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;

        public AdminService(IUnitOfWork unitOfWork, ICompanyService companyService, IUserService userservice, IEmailSender emailSender)
        {
            _unitOfWork = unitOfWork;
            _companyService = companyService;
            _userService = userservice;
            _emailSender = emailSender;
        }
        public async Task<List<EmployerDto>> GetEmployerAsync(Paging? paging)
        {
            var page = new Paging();
            if(paging is not null)
            {
                page = paging;
            }
            return await _unitOfWork.CompanyRepo.GetEmployerWithApprovedAsync(page.PageSize,page.Skip);        
        }
        public async Task<string> SetApprovedCompanyAsync(Guid companyId)
        {
            var company = await _unitOfWork.CompanyRepo.GetByIdAsync(companyId, true);
            if (company is null)
            {
                throw new NotFoundException("company not found", "company-404");
            }
            if (company.IsApproved)
            {
                return "the company already is Approved";
            }
            else
            {
                company.IsApproved = true;
                await _unitOfWork.CompanyRepo.SaveChangesAsync();
                await _emailSender.SendAsync(company.UserId, "Company Status", "your company succesfully was approved");
                return "sucessfully approved";
            }

        }
        public async Task<string> SetNotApprovedCompanyAsync(Guid companyId)
        {
            var company = await _unitOfWork.CompanyRepo.GetByIdAsync(companyId, true);
            if (company is null)
            {
                throw new NotFoundException("company not found", "company-404");
            }
            if (!company.IsApproved)
            {
                return "the company already is not approved";
            }
            else
            {
                company.IsApproved = false;
                await _unitOfWork.CompanyRepo.SaveChangesAsync();
                await _emailSender.SendAsync(company.UserId, "Company Status", "your company is no longer approved");
                return "sucessfully Changed";
            }
        }
        public async Task<CompanyDto> GetMyCompanyDetailAsync(GetCompanyDetailCommand command)
        {
            return await _companyService.GetMyCompanyDetailAsync(command);
        }
        public async Task<List<UserDto>> GetJobSeekers(Paging? paging)
        {
            var page = new Paging();
            if (paging is not null)
            {
                page = paging;
            }
            return await _unitOfWork.UserRepo.GetJobSeekers(page.PageNumber,page.Skip);
        }
        public async Task<UserProfileDto> GetJobseekerDetailAsync(Guid userId,Guid requesterId)
        {
            if (await _unitOfWork.CompanyRepo.HasCompany(userId))
            {
                throw new NotFoundException("JobSeeker not found","JobSeeker-404");
            }
            return await _userService.GetmyProfile(userId, requesterId);
        }

        public async Task<string> ActivateUser(Guid userId)
        {
            var user = await _unitOfWork.userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new NotFoundException("user not found", "user-404");
            }
            if (user.IsActive)
            {
                return "the user already is active";
            }
            if(await _unitOfWork.userManager.IsInRoleAsync(user, "Admin"))
            {
                throw new BadRequestException("you can not Active or InActive the Admin");
            }
            else
            {
                user.IsActive= true;
                await _unitOfWork.userManager.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return "sucessfully activate";
            }
        }
        public async Task<string> DeactivateUser(Guid userId)
        {
            var user = await _unitOfWork.userManager.FindByIdAsync(userId.ToString());
            if (user is null)
            {
                throw new NotFoundException("user not found", "user-404");
            }
            if (await _unitOfWork.userManager.IsInRoleAsync(user, "Admin"))
            {
                throw new BadRequestException("you can not Active or InActive the Admin");
            }
            if (!user.IsActive)
            {
                return "the user already is not active";
            }
            else
            {
                user.IsActive= false;
                await _unitOfWork.userManager.UpdateAsync(user);
                await _unitOfWork.SaveChangesAsync();
                return "sucessfully Deactivate";
            }
        }
        public async Task<List<JobAdDto>> GetAllJobAds(Paging? paging)
        {
            var page = new Paging();
            if(paging is not  null)
            {
                page = paging;
            }
            var result =await _unitOfWork.JobAdsRepo.Pagination(page);
            return result.Select(j => new JobAdDto(j.Title,j.Description,j.Location,j.StartWorkTime,j.EndWorkTIme,j.SalaryMin,j.SalaryMax,j.IsFeatured,j.Status.ToString(),j.EmployementType.ToString(),j.CompanyId,j.Skils,j.Id)).ToList();

        }
        public async Task<string> ActivateJobAd(Guid jobAdId)
        {
            var jobad =await _unitOfWork.JobAdsRepo.GetByIdAsync(jobAdId,true);
            if(jobad is null)
            {
                throw new NotFoundException("JobAd not found","jobAd-404");
            }
            if(jobad.Status != JobAdStatus.Published)
            {
                jobad.Status = JobAdStatus.Published;
                await _unitOfWork.SaveChangesAsync();
                return "Sucessfully Activated";
            }
            else
            {
                return "the jobAd is alredy active"; 
            }

        }
        public async Task<string> ArchiveJobAd(Guid jobAdId)
        {
            var jobad =await _unitOfWork.JobAdsRepo.GetByIdAsync(jobAdId,true);
            if(jobad is null)
            {
                throw new NotFoundException("JobAd not found","jobAd-404");
            }
            if(jobad.Status != JobAdStatus.Archived)
            {
                jobad.Status = JobAdStatus.Archived;
                await _unitOfWork.SaveChangesAsync();
                return "Sucessfully Archived";
            }
            else
            {
                return "the jobAd is alredy Archived"; 
            }

        }
        public async Task<string> CloseJobAd(Guid jobAdId)
        {
            var jobad =await _unitOfWork.JobAdsRepo.GetByIdAsync(jobAdId,true);
            if(jobad is null)
            {
                throw new NotFoundException("JobAd not found","jobAd-404");
            }
            if(jobad.Status != JobAdStatus.Closed)
            {
                jobad.Status = JobAdStatus.Closed;
                await _unitOfWork.SaveChangesAsync();
                return "Sucessfully Closed";
            }
            else
            {
                return "the jobAd is alredy closed"; 
            }

        }
        public async Task<string> MakeProJobAd(Guid jobadId)
        {
            var jobad = await _unitOfWork.JobAdsRepo.GetByIdAsync(jobadId, true);
            if (jobad is null)
            {
                throw new NotFoundException("the jobad not found", "jobad-404");
            }
            if (jobad.FeaturePriority != 1)
            {
                jobad.FeaturePriority = 1;
                await _unitOfWork.SaveChangesAsync();
                return "Succesfully became pro";
            }
            else
            {
                return "the jobAd Is already Pro";
            }
        }
        public async Task<string> MakePlusJobAd(Guid jobadId)
        {
            var jobad = await _unitOfWork.JobAdsRepo.GetByIdAsync(jobadId, true);
            if (jobad is null)
            {
                throw new NotFoundException("the jobad not found", "jobad-404");
            }
            if (jobad.FeaturePriority != 2)
            {
                jobad.FeaturePriority = 2;
                await _unitOfWork.SaveChangesAsync();
                return "Succesfully became plus";
            }
            else
            {
                return "the jobAd Is already plus";
            }
        }
        public async Task<string> MakeNormalJobAd(Guid jobadId)
        {
            var jobad = await _unitOfWork.JobAdsRepo.GetByIdAsync(jobadId, true);
            if (jobad is null)
            {
                throw new NotFoundException("the jobad not found", "jobad-404");
            }
            if (jobad.FeaturePriority != 3)
            {
                jobad.FeaturePriority = 3;
                await _unitOfWork.SaveChangesAsync();
                return "Succesfully became Normall";
            }
            else
            {
                return "the jobAd Is already normall";
            }
        }
        public async Task<AdminDashbordDto> GetAdminDashboardAsync()
        {
            var employersCount =await _unitOfWork.UserRepo.GetEmployerCount();
            var employerIsNotApprovedCount = await _unitOfWork.CompanyRepo.GetNotApprovedEmployerCountAsync();
            var jObSeekersCount = await _unitOfWork.UserRepo.GetJobSeekerCount();
            var activeJobAdsCount = await _unitOfWork.JobAdsRepo.ActiveJobAdCount();
             var deactiveJobAdsCount = await _unitOfWork.JobAdsRepo.DeactiveJobAdCount();
            var submittedAppsCount = await _unitOfWork.ApplicationRepo.GetAppsCountByStatusAsync(ApplicationStatus.Submitted);
            var inReviewAppsCount = await _unitOfWork.ApplicationRepo.GetAppsCountByStatusAsync(ApplicationStatus.InReview);
            var interviewAppsCount = await _unitOfWork.ApplicationRepo.GetAppsCountByStatusAsync(ApplicationStatus.Interview);
            var acceptedAppsCount = await _unitOfWork.ApplicationRepo.GetAppsCountByStatusAsync(ApplicationStatus.Accepted);
            var rejectedAppsCount = await _unitOfWork.ApplicationRepo.GetAppsCountByStatusAsync(ApplicationStatus.Rejected);
            var canceledAppsCount = await _unitOfWork.ApplicationRepo.GetAppsCountByStatusAsync(ApplicationStatus.canceled);
            return new AdminDashbordDto(activeJobAdsCount,deactiveJobAdsCount,submittedAppsCount,inReviewAppsCount,interviewAppsCount,acceptedAppsCount,rejectedAppsCount,canceledAppsCount,employerIsNotApprovedCount,employersCount,jObSeekersCount);
        }
       

       
    }
}
