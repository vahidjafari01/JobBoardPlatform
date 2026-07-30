using JobBoardPlatfomr.Domain.Abstractions;
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

        public async Task<List<EmployerDto>> GetEmployerWithApprovedAsync(int take,int skip)
        {
            return await _context.Companies.Include(c => c.Owner).Select(c => new EmployerDto(c.Owner.Id,c.Owner.FirstName,c.Owner.LastName,c.Owner.CreatedAt,c.Owner.ModifiedAt,c.Owner.IsDeleted,c.IsApproved,c.Id)).Skip(skip).Take(take).ToListAsync();
        }
        public async Task<bool> HasCompany(Guid userId) 
        {
            return await _context.Companies.AnyAsync(c => c.UserId == userId);
        
        }
        public async Task<int> GetNotApprovedEmployerCountAsync()
        {
            return await _context.Companies.CountAsync(c => c.IsApproved == false);
        }
    }
}
