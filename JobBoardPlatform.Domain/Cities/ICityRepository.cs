using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Cities
{
    public interface ICityRepository:IGenericRepository<City>
    {
        Task<City?> GetCityWithJobAdsAsync(Guid id);
    }
}
