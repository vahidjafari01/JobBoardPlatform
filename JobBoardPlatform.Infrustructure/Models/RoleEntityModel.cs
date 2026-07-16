using JobBoardPlatform.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Models
{
    public class RoleEntityModel : IEntityTypeConfiguration<RoleEntity>
    {
        public void Configure(EntityTypeBuilder<RoleEntity> builder)
        {
            builder.Property(a => a.CreatedAt).HasDefaultValueSql("GetDate()");
            builder.HasData(
                new RoleEntity()
                {
                    Id = new Guid("290aed19-1878-48cc-9028-ed7419a25b52"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"

                },
                new RoleEntity()
                {
                    Id = new Guid("3e9f489c-e97f-40dc-85c3-76ce5378303d"),
                    Name = "JobSeeker",
                    NormalizedName = "JOBSEEKER"
                },
                new RoleEntity()
                {
                    Id = new Guid("e5e54fe9-0f12-4b07-9243-3471ebe491bc"),
                    Name = "Employer",
                    NormalizedName = "EMPLOYER"
                }
                );
        }
    }
}
