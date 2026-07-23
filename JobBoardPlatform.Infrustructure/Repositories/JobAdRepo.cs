using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.enums;
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
            var a = await _context.JobAds.Include(j => j.Company).Include(j => j.Category).FirstOrDefaultAsync(j => j.Id == jobadId);
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
        public async Task<JobAd?> GetJobAdDetail(Guid jobAdId)
        {
            return await _context.JobAds.Include(j => j.Applications).Include(j => j.Payments).FirstOrDefaultAsync(j => j.Id == jobAdId);
        }
        public async Task<List<JobAd>> GetJobAdsBYFilter(GetJobAdCommand command,int take,int skip)
        {
            var query = _context.JobAds
                .AsNoTracking()
                .Include(j => j.Company)
                .Include(j => j.City)
                .Where(j => j.Status == JobAdStatus.Published)
                .AsQueryable();

            if (command.ProvinceId != null)
            {
                query = query.Where(j => j.City.ProvinceId == command.ProvinceId);
            }

            if (command.CityId != null)
            {
                query = query.Where(j => j.CityId == command.CityId);
            }

            if (command.CategoryId != null)
            {
                query = query.Where(j => j.CategoryId == command.CategoryId);
            }

            if (command.CompanyId != null)
            {
                query = query.Where(j => j.CompanyId == command.CompanyId);
            }

            if (command.EmployementType != null)
            {
                query = query.Where(j => j.EmployementType == command.EmployementType);
            }

            if (command.Title != null)
            {
                query = query.Where(j => EF.Functions.Like(j.Title, $"%{command.Title}%"));
            }
            if (command.SalaryMin != null)
            {
                query = query.Where(j =>j.SalaryMin >= command.SalaryMin);
            }
            if (command.SalaryMax != null)
            {
                query = query.Where(j =>j.SalaryMax <= command.SalaryMax);
            }

            if (command.Skils != null && command.Skils.Count >= 1)
            {
                query = query.Where(j => j.Skils.Any(s => command.Skils.Contains(s)));
            }


            return await query.OrderBy(j => j.FeaturePriority).ThenByDescending(j => j.CreatedAt).Skip(skip).Take(take).ToListAsync(); 
        }
        public async Task<JobAd?> GetJoinedJobAd(Guid JobAdId)
        {
            return await _context.JobAds.Include(j => j.Category).Include(j => j.Company).AsNoTracking().FirstOrDefaultAsync(j => j.Id == JobAdId);
        }

    }
}
