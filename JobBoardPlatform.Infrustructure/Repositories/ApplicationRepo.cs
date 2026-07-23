using JobBoardPlatform.Domain.Applications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class ApplicationRepo : GenericRepository<Application>, IApplicationREpository
    {
        public ApplicationRepo(AppDbContext context) : base(context)
        {
        }
        public async Task<Application?> GetDetailAppbyIdAsync(Guid appid)
        {
            return await _context.Applications.Include(a => a.User).FirstOrDefaultAsync(a => a.Id == appid);
        }
        public async Task<bool> AlreadyExsist(Guid userId, Guid JobadId)
        {
            return await _context.Applications.AnyAsync(a => a.UserId== userId && a.JobAdId ==JobadId);
        }

        public async Task<List<Application>> GetJoinedApps(Guid userId)
        {
            return await _context.Applications.Include(a => a.JObAd).ThenInclude(j => j.Company).Where(a => a.UserId == userId).ToListAsync();
        }
        public async Task<Application?> GetJoinedAppByAppId(Guid appId)
        {
            return await _context.Applications.Include(a => a.User).Include(a => a.JObAd).ThenInclude(j => j.Company).FirstOrDefaultAsync(a => a.Id == appId);
        }
    }
}
