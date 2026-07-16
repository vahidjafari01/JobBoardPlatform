using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Cities;
using JobBoardPlatform.Domain.Provinces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IProvinceService
    {
        Task<List<ProvincDto>> GetAllAsync();
        Task<List<CityDto>> GetAllCityByIdAsync(Guid id);

    }
}
