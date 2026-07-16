using JobBoardPlatform.Domain.Applications;
using Microsoft.EntityFrameworkCore;
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
    }
}
