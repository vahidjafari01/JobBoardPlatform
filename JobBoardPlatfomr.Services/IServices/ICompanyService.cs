using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.OutPutDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface ICompanyService
    {
        Task<Guid> CreateCompany(AddCompanyCommand command);
        Task UpdateCompanyAsync(UpdateCompanyComand command);
        Task<CompanyDto> GetMyCompanyDetailAsync(GetCompanyDetailCommand command);
        Task SetApprovedCompanyAsync(Guid companyId);
    }
}
