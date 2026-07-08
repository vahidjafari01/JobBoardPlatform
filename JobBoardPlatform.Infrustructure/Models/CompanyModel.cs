using JobBoardPlatform.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Models
{
    public class CompanyModel : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(c => c.Name).HasColumnType("nvarchar(50)");
            builder.Property(c => c.Name).HasMaxLength(50);
            builder.Property(c => c.Location).HasMaxLength(100);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.Website).HasMaxLength(100);
            builder.Property(c => c.Location).HasColumnType("nvarchar(100)");
            builder.Property(c => c.Website).HasColumnType("nvarchar(100)");
            builder.Property(c => c.Description).HasColumnType("nvarchar(500)");

        }
    }
}
