using JobBoardPlatform.Domain.Provinces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class ProvinceRepo : GenericRepository<Province>, IProvinceRepository
    {
        public ProvinceRepo(AppDbContext context) : base(context)
        {
        }


        public async Task<Province?> GetProvinceWithCitiesAsync(Guid id)
        {
            return await _context.Provinces
                .Include(p => p.Cities)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

    }
}
