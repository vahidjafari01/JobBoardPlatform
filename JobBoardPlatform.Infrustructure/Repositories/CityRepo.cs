using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Cities;
using JobBoardPlatform.Domain.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class CityRepo : GenericRepository<City>, ICityRepository
    {
        public CityRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<City?> GetCityWithJobAdsAsync(Guid id)
        {
            return await _context.Cities
                .Include(p => p.JobAds)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
