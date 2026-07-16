using JobBoardPlatform.Domain.Companies;
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
    public class UserModel : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Company).WithOne(c => c.Owner).HasForeignKey<Company>(c => c.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(u => u.Applications).WithOne(a => a.User).HasForeignKey(a => a.UserId).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(u => u.ProfilePhoto).WithOne().HasForeignKey<User>(u => u.ProfilePhotoId);
            builder.Property(u => u.FirstName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);
            builder.Property(u => u.IsDeleted).HasDefaultValue(false);
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.FirstName).HasColumnType("nvarchar(30)");
            builder.Property(u => u.LastName).HasColumnType("nvarchar(30)");
        }
    }
}
