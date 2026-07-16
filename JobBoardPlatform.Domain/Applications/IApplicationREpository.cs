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
    }
}
