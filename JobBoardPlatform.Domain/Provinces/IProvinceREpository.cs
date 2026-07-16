using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Provinces
{
    public interface IProvinceRepository:IGenericRepository<Province>
    {
        Task<Province?> GetProvinceWithCitiesAsync(Guid id);
    }
}
