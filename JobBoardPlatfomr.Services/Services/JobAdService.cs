using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class JobAdService:IJobAdServices
    {

        private readonly IUnitOfWork _unitofwork;

        public JobAdService(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<Guid> AddJobAd(JobAdCreateCommand command)
        {
            var user = await _unitofwork.userManager.FindByIdAsync(command.RequesterId.ToString());
            if (user == null)
            {
                throw new NotFoundException("user not found", "user-404");
            }
            if (!await _unitofwork.userManager.IsInRoleAsync(user, "Employer"))
            {
                throw new PermisionException("you dont have any company", "403");
            }
            var company = await _unitofwork.CompanyRepo.GetByUserId(user.Id);
            if (company == null)
            {
                throw new NotFoundException("company not found", "company-404");
            }
            if (!company.IsApproved)
            {
                throw new PermisionException("your company is not approved...", "company-403");
            }
            var category = await _unitofwork.JobCategoryRepo.GetByIdAsync(command.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("category not found", "category-404");
            }
            var city = await _unitofwork.CityRepo.GetByIdAsync(command.CityId);
            if (city == null)
            {
                throw new NotFoundException("city not found", "city-404");
            }
            if (string.IsNullOrEmpty(command.Title) || command.Title.Length < 3)
            {
                throw new BadRequestException("title must be longer than 2 characters");
            }
            if (string.IsNullOrEmpty(command.Description) || command.Description.Length < 10)
            {
                throw new BadRequestException("Description must be longer than 10 characters");
            }
            if (!Enum.TryParse<EmploymentType>(command.EmployementType, true, out var resultstatus))
            {
                throw new BadRequestException("the employment type is not valid");
            }
            var jobAd = new JobAd(command.Title, command.Description, command.Location, command.SalaryMin, command.SalaryMax, 3, JobAdStatus.Published, resultstatus, company.Id, command.CategoryId, command.CityId, command.Skils, command.StartWorkTime, command.EndWorkTIme);
            await _unitofwork.JobAdsRepo.AddAsync(jobAd);
            await _unitofwork.JobAdsRepo.SaveChangesAsync();
            return jobAd.Id;
        }

        public async Task UpdateJobAd(JobEditCommand command)
        {
            var company = await _unitofwork.JobAdsRepo.GetCompanyWithJobAdIdAsync(command.JobadId);
            if (company == null)
            {
                throw new NotFoundException("company not found", "company-404");
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
            var category = await _unitofwork.JobCategoryRepo.GetByIdAsync(command.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("category not found", "category-404");
            }
            var city = await _unitofwork.CityRepo.GetByIdAsync(command.CityId);
            if (city == null)
            {
                throw new NotFoundException("city not found", "city-404");
            }
            if (string.IsNullOrEmpty(command.Title) || command.Title.Length < 3)
            {
                throw new BadRequestException("title must be longer than 2 characters");
            }
            if (string.IsNullOrEmpty(command.Description) || command.Description.Length < 10)
            {
                throw new BadRequestException("Description must be longer than 10 characters");
            }
            if (!Enum.TryParse<EmploymentType>(command.EmployementType, true, out var resultstatus))
            {
                throw new BadRequestException("the employment type is not valid");
            }
            if (!Enum.TryParse<JobAdStatus>(command.Jobadstatus, true, out var jobStatus))
            {
                throw new BadRequestException("the jobStatus type is not valid");
            }

            var Jobad = await _unitofwork.JobAdsRepo.GetByIdAsync(command.JobadId, true);
            Jobad.Title = command.Title;
            Jobad.Description = command.Description;
            Jobad.Location = command.Location;
            Jobad.StartWorkTime = command.StartWorkTime;
            Jobad.EndWorkTIme = command.EndWorkTIme;
            Jobad.CategoryId = command.CategoryId;
            Jobad.CityId = command.CityId;
            Jobad.EmployementType = resultstatus;
            Jobad.Status = jobStatus;
            Jobad.SalaryMax = command.SalaryMax;
            Jobad.SalaryMin = command.SalaryMin;
            Jobad.Skils = command.Skils;
            await _unitofwork.JobAdsRepo.SaveChangesAsync();
        }
        public async Task<List<JobAdDto>> GetJobAds(Paging paging)
        {
            var jobads = await _unitofwork.JobAdsRepo.GetJObAdsPaging(paging.PageNumber, paging.Skip,j => j.Status == JobAdStatus.Published);
             return jobads.Select(j => new JobAdDto
            {
                Title = j.Title,
                Description = j.Description,
                Location = j.Location,
                StartWorkTime = j.StartWorkTime,
                EndWorkTIme = j.EndWorkTIme,
                SalaryMin = j.SalaryMin,
                SalaryMax = j.SalaryMax,
                IsFeatured = j.IsFeatured,
                Status = j.Status.ToString(),
                EmployementType = j.EmployementType.ToString(),
                CompanyId = j.CompanyId,
                Skils = j.Skils,
            }).ToList();
        }
        public async Task DeleteJobAd(JObAdDeleteCommand command)
        {
            var company = await _unitofwork.JobAdsRepo.GetCompanyWithJobAdIdAsync(command.JObAdID);
            if (company == null)
            {
                throw new NotFoundException("jobad not found", "jobad-404");
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
            var jobad =await _unitofwork.JobAdsRepo.GetByIdAsync(command.JObAdID,true);
            jobad.Status = JobAdStatus.Closed;
            await _unitofwork.JobAdsRepo.SaveChangesAsync();
        }
        public async Task<List<JobAdDto>> GetMyJobAds(GetMyJobAdsCommand command)
        {
            var company = await _unitofwork.CompanyRepo.GetByIdAsync(command.companyId);
            if(company is null)
            {
                throw new NotFoundException("company not found", "company-404");
            }
            if (!await IsAdmin(command.RequesterId))
            {
                if (company.UserId != command.RequesterId)
                {
                    throw new PermisionException("this jobAd Does not belong to you", "jobAd-403");
                }
            }
            var jobads = await _unitofwork.JobAdsRepo.QueryAsync(j => j.CompanyId == command.companyId );
            return jobads.Select(j => new JobAdDto
            {
                Title = j.Title,
                Description = j.Description,
                Location = j.Location,
                StartWorkTime = j.StartWorkTime,
                EndWorkTIme = j.EndWorkTIme,
                SalaryMin = j.SalaryMin,
                SalaryMax = j.SalaryMax,
                IsFeatured = j.IsFeatured,
                Status = j.Status.ToString(),
                EmployementType = j.EmployementType.ToString(),
                CompanyId = j.CompanyId,
                Skils = j.Skils,
            }).ToList();
        }

        public async Task ActiveMyJObAd(ActiveJobAdCommand command)
        {
            var company = await _unitofwork.JobAdsRepo.GetCompanyWithJobAdIdAsync(command.JobId);
            if (company == null)
            {
                throw new NotFoundException("jobad not found", "jobad-404");
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
            var jobad = await _unitofwork.JobAdsRepo.GetByIdAsync(command.JobId,true);
            if (jobad == null)
            {
                throw new NotFoundException("job not found", "jobad-404");
            }
            if(jobad.Status == JobAdStatus.Closed)
            {
                jobad.Status = JobAdStatus.Published;
            }
            await _unitofwork.JobAdsRepo.SaveChangesAsync();

        }
        public async Task ArchiveMyJobAd(ArchiveMyJobAdCommand command)
        {
            var company = await _unitofwork.JobAdsRepo.GetCompanyWithJobAdIdAsync(command.JobId);
            if (company == null)
            {
                throw new NotFoundException("jobAD not found", "JobAd-404");
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
            var jobad = await _unitofwork.JobAdsRepo.GetByIdAsync(command.JobId, true);
            if (jobad == null)
            {
                throw new NotFoundException("jobAd not found", "jobad-404");
            }
            jobad.Status = JobAdStatus.Archived;
           
            await _unitofwork.JobAdsRepo.SaveChangesAsync();


        }
        public async Task<JobAdDetail> GetDetailJobAd(GetJObAdDetailCommand command)
        {
            var company = await _unitofwork.JobAdsRepo.GetCompanyWithJobAdIdAsync(command.JobAdId);
            if (company == null)
            {
                throw new NotFoundException("jobAd not found", "jobAd-404");
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
           
            var jobad = await _unitofwork.JobAdsRepo.GetJobAdDetail(command.JobAdId);
            if (jobad == null)
            {
                throw new NotFoundException("jobAd not found", "jobad-404");
            }
           
            var category =await _unitofwork.JobCategoryRepo.GetByIdAsync(jobad.CategoryId);
            if (category == null)
            {
                throw new NotFoundException("category not found", "category-404");
            }
            var jobAdDetail = new JobAdDetail(jobad.Title,jobad.Description,jobad.Location,jobad.StartWorkTime,jobad.EndWorkTIme,jobad.SalaryMin,jobad.SalaryMax,jobad.IsFeatured,jobad.Status.ToString(),jobad.EmployementType.ToString(),jobad.Skils,jobad.FeaturePriority,jobad.FeaturedUntil,jobad.Applications.Count(),jobad.Payments,category.Name);
            return jobAdDetail;

        }
        private async Task<bool> IsAdmin(Guid requesterid)
        {
            var user =await _unitofwork.userManager.FindByIdAsync(requesterid.ToString());
            return await _unitofwork.userManager.IsInRoleAsync(user,"Admin");
        }






    }
}
