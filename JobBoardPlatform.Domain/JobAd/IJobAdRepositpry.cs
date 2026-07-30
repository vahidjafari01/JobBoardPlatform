using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.entities
{
    public interface IJobAdRepositpry:IGenericRepository<JobAd>
    {
        Task<Company?> GetCompanyWithJobAdIdAsync(Guid jobadId);
        Task<List<JobAd>> GetJObAdsPaging(int take, int skip, Expression<Func<JobAd, bool>> filter);
        Task<List<Application>> GetApplicationsJobAd(Guid jobadId);

        Task<JobAd?> GetJobAdDetail(Guid jobAdId);
        Task<List<JobAd>> GetJobAdsBYFilter(GetJobAdCommand command, int take, int skip);

        Task<JobAd?> GetJoinedJobAd(Guid JobAdId);
        Task<int> ActiveJobAdCount();
        Task<int> DeactiveJobAdCount();
    }
}
