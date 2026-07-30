using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Applications
{
    public interface IApplicationREpository:IGenericRepository<Application>
    {
        Task<Application?> GetDetailAppbyIdAsync(Guid appid);
        Task<bool> AlreadyExsist(Guid userId, Guid JobadId);
        Task<List<Application>> GetJoinedApps(Guid userId);
        Task<Application?> GetJoinedAppByAppId(Guid appId);
        Task<int> GetAppsCountByStatusAsync(ApplicationStatus status);
    }
}
