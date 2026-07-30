using JobBoardPlatfomr.Domain.Abstractions;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IAdminService
    {
        Task<string> SetApprovedCompanyAsync(Guid companyId);
        Task<string> SetNotApprovedCompanyAsync(Guid companyId);
        Task<List<EmployerDto>> GetEmployerAsync();
        Task<List<UserDto>> GetJobSeekers(Paging? paging);
        Task<string> ActivateUser(Guid userId);
        Task<string> DeactivateUser(Guid userId);
        Task<List<JobAdDto>> GetAllJobAds(Paging? paging);
        Task<string> ActivateJobAd(Guid jobAdId);
        Task<string> ArchiveJobAd(Guid jobAdId);
        Task<string> CloseJobAd(Guid jobAdId);
        Task<string> MakeProJobAd(Guid jobadId);
        Task<string> MakePlusJobAd(Guid jobadId);
        Task<string> MakeNormalJobAd(Guid jobadId);
        Task<AdminDashbordDto> GetAdminDashboardAsync();
    }
}
