using JobBoardPlatfomr.Services.InputDtos;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IJobAdServices
    {
        Task<Guid> AddJobAd(JobAdCreateCommand command);

        Task UpdateJobAd(JobEditCommand command);
        Task<List<JobAdDto>> GetJobAds(Paging paging);
        Task DeleteJobAd(JObAdDeleteCommand command);
        Task<List<JobAdDto>> GetMyJobAds(GetMyJobAdsCommand command);
        Task ActiveMyJObAd(ActiveJobAdCommand command);
        Task ArchiveMyJobAd(ArchiveMyJobAdCommand command);
        Task<JobAdDetail> GetDetailJobAd(GetJObAdDetailCommand command);
        Task<List<JobAdViewModel>> GetJobAdsForCustomersAsync(GetJObAdFilterCommand cmd, Paging? paging);

        Task<JobAdDetailForCustomer> GetJobAdDetailForCustomerAsync(Guid JobAdId);
        Task<string> MakePlusJobAd(Guid JobAdId, Guid requesterId);
        Task<string> MakeProJobAd(Guid JobAdId, Guid requesterId);
        Task UpdateExpiredJobs();


    }
}
