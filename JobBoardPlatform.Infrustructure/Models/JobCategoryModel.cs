using JobBoardPlatform.Domain.JobCategories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Models
{
    public class JobCategoryModel : IEntityTypeConfiguration<JobCategory>
    {
        

        public void Configure(EntityTypeBuilder<JobCategory> builder)
        {
            builder.Property(a => a.Name).IsRequired().HasColumnType("nvarchar(50)").HasMaxLength(50);
        }
    }
}
