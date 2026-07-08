using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.Attachment;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.JobCategories;
using JobBoardPlatform.Domain.Payments;
using JobBoardPlatform.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure
{
    public class AppDbContext:IdentityDbContext<User,RoleEntity,Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<JobAd> JobAds { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Company> Companies{ get; set; }
        public DbSet<JobCategory> JobCategories{ get; set; }
        public DbSet<Payment> Payments{ get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
