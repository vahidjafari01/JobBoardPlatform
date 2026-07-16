using JobBoardPlatform.Domain.entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Models
{
    public class JobAdModel : IEntityTypeConfiguration<JobAd>
    {
        public void Configure(EntityTypeBuilder<JobAd> builder)
        {
            builder.HasMany(j => j.Applications).WithOne(a => a.JObAd).HasForeignKey(a => a.JobAdId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(j => j.Payments).WithOne(a => a.JobAd).HasForeignKey(a => a.JobAdId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(j => j.Company).WithMany(c => c.JobAds).HasForeignKey(j => j.CompanyId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(j => j.Category).WithMany(c => c.JobAds).HasForeignKey(j => j.CategoryId).OnDelete(DeleteBehavior.NoAction);
            builder.Property(j => j.SalaryMin).HasPrecision(10,2);
            builder.Property(j => j.SalaryMax).HasPrecision(10,2);
            builder.Property(j => j.Description).HasColumnType("nvarchar(400)");
            builder.Property(j => j.Location).HasColumnType("nvarchar(150)");
            builder.Property(j => j.Title).HasColumnType("nvarchar(100)");
            
        }
    }
}
