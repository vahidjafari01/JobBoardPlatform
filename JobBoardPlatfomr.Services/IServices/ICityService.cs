using JobBoardPlatform.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface ICityService
    {
        Task<List<JobAd>> GetJobAdsAsync(Guid cityId);
    }
}
