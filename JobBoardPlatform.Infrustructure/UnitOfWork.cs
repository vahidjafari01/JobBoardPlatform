using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.Cities;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.JobCategories;
using JobBoardPlatform.Domain.Notifications;
using JobBoardPlatform.Domain.Payments;
using JobBoardPlatform.Domain.Provinces;
using JobBoardPlatform.Domain.Users;
using JobBoardPlatform.Infrustructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore.Storage;
    using Microsoft.Extensions.Configuration;
    using System.Threading;
    using System.Threading.Tasks;

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private IDbContextTransaction? _transaction;

        public IUserREpository UserRepo { get; }
        public ICompanyRepository CompanyRepo { get; }
        public ICityRepository CityRepo { get; }
        public IProvinceRepository ProvinceRepo { get; }
        public IJobAdRepositpry JobAdsRepo { get; }
        public IJobCategoryRepository JobCategoryRepo { get; }
        public INotificationRepo NotificationsRepo { get; }
        public IPaymentRepository PaymentsRepo { get; }
        public IAttachRepo AttacheRepo { get; }
        public IConfiguration configuration;

        public IApplicationREpository ApplicationRepo { get; }
        public IRefreshTokenRepo RefreshTokenRepo{ get; }
        public UserManager<User> userManager{ get;  }
        public RoleManager<RoleEntity> roleManager{ get; }


        public UnitOfWork(AppDbContext appDbContext, RoleManager<RoleEntity> roleManager, UserManager<User> userManager, IConfiguration configuration)
        {
            _appDbContext = appDbContext;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.configuration = configuration;

            UserRepo = new UserRepository(appDbContext,configuration);
            CompanyRepo = new CompanyRepo(appDbContext);
            CityRepo = new CityRepo(appDbContext);
            ProvinceRepo = new ProvinceRepo(appDbContext);
            JobAdsRepo = new JobAdRepo(appDbContext);
            JobCategoryRepo = new JobCategoryRepo(appDbContext);
            NotificationsRepo = new NotificationRepo(appDbContext);
            PaymentsRepo = new PaymentRepo(appDbContext);
            ApplicationRepo = new ApplicationRepo(appDbContext);
            AttacheRepo = new AttachREpo(appDbContext);
            RefreshTokenRepo = new RefreshTokenRepo(appDbContext);
        }

        public async Task BeginTransactionAsync()
        {
            if (_transaction != null)
                return;

            _transaction = await _appDbContext.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                return;

            await _appDbContext.SaveChangesAsync();
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
                return;

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task SaveChangesAsync()
        {
           await _appDbContext.SaveChangesAsync();
        }

    }


}
