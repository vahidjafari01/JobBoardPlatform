using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Cities
{
    public class City : BaseEntity
    {
        public string Name { get; set; }

        public Guid ProvinceId { get; set; }
        public List<JobAd> JobAds { get; set; }
        public List<Company> Companies { get; set; }

    }
}
