using JobBoardPlatfomr.Services.BussinesExceptions;
using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class CityService: ICityService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CityService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<JobAd>> GetJobAdsAsync(Guid cityId)
        {
            var city =await _unitOfWork.CityRepo.GetCityWithJobAdsAsync(cityId);
            if (city == null) {
                throw new NotFoundException("City not found", "city-404");
            }
            return city.JobAds;
        }



    }
}
