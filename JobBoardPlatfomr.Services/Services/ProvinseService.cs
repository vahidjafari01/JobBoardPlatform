using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatfomr.Services.OutPutDtos;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Cities;
using JobBoardPlatform.Domain.Provinces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class ProvinseService : IProvinceService
    {
        private IUnitOfWork _unitofWork;
        public ProvinseService(IUnitOfWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public async Task<List<ProvincDto>> GetAllAsync()
        {
            var provinces =await _unitofWork.ProvinceRepo.GetAllAsync();
            return provinces.Select(p =>  new ProvincDto
            {
                Name = p.Name,
                Id = p.Id,
            }).ToList();
        }

        public async Task<List<CityDto>> GetAllCityByIdAsync(Guid id)
        {
            var province = await _unitofWork.ProvinceRepo.GetProvinceWithCitiesAsync(id);
            if (province == null) {
                throw new NotFoundException("Province Not found","Province-404");
            }
            return province.Cities.Select(c => new CityDto { Name = c.Name , Id = c.Id}).ToList();
        }
    }
}
