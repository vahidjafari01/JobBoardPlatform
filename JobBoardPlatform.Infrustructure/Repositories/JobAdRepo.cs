using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class JobAdRepo : GenericRepository<JobAd>, IJobAdRepositpry
    {
        public JobAdRepo(AppDbContext context) : base(context)
        {
        }
        public async Task<Company?> GetCompanyWithJobAdIdAsync(Guid jobadId)
        {
            var a = await _context.JobAds.Include(j => j.Company).FirstOrDefaultAsync(j => j.Id == jobadId);
            return a.Company;
        }
        public async Task<List<JobAd>> GetJObAdsPaging(int take,int skip,Expression<Func<JobAd,bool>> filter)
        {
            return await _context.JobAds.Where(filter).OrderBy(j => j.FeaturePriority).ThenByDescending(j => j.CreatedAt).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<List<Application>> GetApplicationsJobAd(Guid jobadId) {

            var jobad = await _context.JobAds.Include(j => j.Applications).ThenInclude(a => a.User).FirstOrDefaultAsync(j => j.Id == jobadId);
            return jobad.Applications ?? new List<Application>();
        
        }
    }
}
