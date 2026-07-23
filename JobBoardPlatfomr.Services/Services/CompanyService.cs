using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.BaseExceptions;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace JobBoardPlatfomr.Services.Services
{
    public class CompanyService:ICompanyService
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IAttachService _attachService;

        public CompanyService(IUnitOfWork unitofwork, IAttachService attachService)
        {
            _unitofwork = unitofwork;
            _attachService = attachService;
        }
        public async Task<Guid> CreateCompany(AddCompanyCommand command)
        {
            await _unitofwork.BeginTransactionAsync();
            try
            {
                var user = await _unitofwork.userManager.FindByIdAsync(command.UserId.ToString());
                if (user == null)
                {
                    throw new NotFoundException("User not found", "user-404");
                }

                var isEmployer = await _unitofwork.userManager.IsInRoleAsync(user, "Employer");
                if (isEmployer)
                {
                    throw new PermisionException("you already are a employer and have a company", "403");
                }
                await _unitofwork.userManager.RemoveFromRoleAsync(user, "JobSeeker");
                await _unitofwork.userManager.AddToRoleAsync(user, "Employer");
                await ValidateAsync(command.Name, command.Location);
                var city = await _unitofwork.CityRepo.GetByIdAsync(command.CityId);
                if (city == null)
                {
                    throw new NotFoundException("City NOt found", "City-404");
                }

                var company = new Company(command.Name, command.Description, command.Website, command.Location, command.UserId, command.CityId);

                await _unitofwork.CompanyRepo.AddAsync(company);

                await _unitofwork.CompanyRepo.SaveChangesAsync();

                await _unitofwork.CommitTransactionAsync();

                return company.Id;

            }
            catch (BaseException ex)
            {

                await _unitofwork.RollbackTransactionAsync();
                throw new BaseException("error in creating Company", "company-400", ex);
            }
            catch (Exception ex)
            {
                await _unitofwork.RollbackTransactionAsync();
                throw new Exception("error in creating Company",ex);
            }
        }

        public async Task UpdateCompanyAsync(UpdateCompanyComand command)
        {
            var user = await _unitofwork.userManager.FindByIdAsync(command.RequesterId.ToString());
            if(user is null)
            {
                throw new NotFoundException("user was not found ","User 404");
            }
            var company = await _unitofwork.CompanyRepo.GetByIdAsync(command.CompanyId,true);
            if (company is null)
            {
                throw new NotFoundException("company was not found ", "Company-404");
            }
            if(company.UserId != user.Id)
            {
                throw new PermisionException("this company does not belong to you","company-400");
            }
            await ValidateAsync(command.Name, command.Location);


            company.Name = command.Name;
            company.Location = command.Location;
            company.Description = command.Description;
            company.Website = command.Website;
            company.ModifiedAt = DateTime.UtcNow;

            await _unitofwork.CompanyRepo.SaveChangesAsync();
        }
        private async Task ValidateAsync(string name, string location) {

            if (string.IsNullOrEmpty(name))
            {
                throw new BadRequestException("name can not be null");
            }
            if (string.IsNullOrEmpty(location))
            {
                throw new BadRequestException("Location can not be null");
            }
            if (name.Length < 3)
            {
                throw new BadRequestException("name must be longer than 2 characters");
            }
            if (location.Length < 10)
            {
                throw new BadRequestException("location must be longer than 10 characters");
            }
        }
        public async Task<CompanyDto> GetMyCompanyDetailAsync(GetCompanyDetailCommand command)
        {
            var user = await _unitofwork.userManager.FindByIdAsync(command.RequesterId.ToString());
            if (user == null)
            {
                throw new NotFoundException("User not found", "user-404");
            }
            var company = await _unitofwork.CompanyRepo.GetCompanywithUserAsync(command.CompanyId);
            if (company is null)
            {
                throw new NotFoundException("company was not found ", "Company-404");
            }
            if (company.UserId != user.Id)
            {
                throw new PermisionException("this company does not belong to you", "company-400");
            }
            CompanyDto result = new CompanyDto(company.Name,company.Description,company.Website,company.Location,company.Owner.FirstName +" " +company.Owner.LastName,company.CreatedAt,company.ModifiedAt,company.LogoId);
            return result;
        }

        public async Task SetApprovedCompanyAsync(Guid companyId)
        {
            var company =await _unitofwork.CompanyRepo.GetByIdAsync(companyId,true);
            if (company is null)
            {
                throw new NotFoundException("company not found","company-404");
            }
            company.IsApproved = true;
            await _unitofwork.CompanyRepo.SaveChangesAsync();
        }

        public async Task<Guid> UploadCompanyLogo(Guid companyId, Guid requesterId, IFormFile file)
        {
            await _unitofwork.BeginTransactionAsync();
            try
            {
                var company =await _unitofwork.CompanyRepo.GetByUserId(companyId);
                if (company is null)
                {
                    throw new NotFoundException("company not found", "company-404");
                }
                if (!await IsAdmin(requesterId))
                {
                    if (company.UserId != requesterId)
                    {
                        throw new PermisionException("This company does not belong to you.", "company-403");
                    }
                }
                var AttachId = await _attachService.UploadAsync(file);
                if(company.LogoId != null)
                {
                    await _attachService.HardDeleteAttachmentAsync(company.LogoId.Value);
                }
                company.LogoId = AttachId;
                await _unitofwork.SaveChangesAsync();
                await _unitofwork.CommitTransactionAsync();
                return AttachId;
            }
            catch (NotFoundException ex)
            {
                await _unitofwork.RollbackTransactionAsync();
                throw ex;
            }
            catch (PermisionException ex)
            {
                await _unitofwork.RollbackTransactionAsync();
                throw ex;
            }
            catch (Exception ex)
            {
                await _unitofwork.RollbackTransactionAsync();
                throw ex;
            }
        }
        private async Task<bool> IsAdmin(Guid requesterid)
        {
            var user = await _unitofwork.userManager.FindByIdAsync(requesterid.ToString());
            return await _unitofwork.userManager.IsInRoleAsync(user, "Admin");
        }


    }
   
}
