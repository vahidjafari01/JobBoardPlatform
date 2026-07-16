using JobBoardPlatform.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class CompanyRepo : GenericRepository<Company>, ICompanyRepository
    {
        public CompanyRepo(AppDbContext context) : base(context)
        {
        }
        public async Task<Company?> GetCompanywithUserAsync(Guid companyid)
        {
            return await _context.Companies.Include(c => c.Owner).FirstOrDefaultAsync(c => c.Id == companyid);

        }
        public async Task<Company?> GetByUserId(Guid userId) {
        return await _context.Companies.FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
