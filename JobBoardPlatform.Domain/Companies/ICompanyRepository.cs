using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Companies
{
    public interface ICompanyRepository:IGenericRepository<Company>
    {
        Task<Company?> GetCompanywithUserAsync(Guid companyid);
        Task<Company?> GetByUserId(Guid userId);
    }
}
