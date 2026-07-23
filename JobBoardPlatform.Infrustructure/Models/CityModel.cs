using JobBoardPlatform.Domain.Cities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Models
{
    public class CityMOdel : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasMany(c => c.JobAds).WithOne(j => j.City).HasForeignKey(j => j.CityId);
            builder.HasMany(c => c.Companies).WithOne().HasForeignKey(c => c.CityId);
        }
    }
}
