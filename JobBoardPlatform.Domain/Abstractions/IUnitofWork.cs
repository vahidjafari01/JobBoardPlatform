using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.Cities;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.JobCategories;
using JobBoardPlatform.Domain.Notifications;
using JobBoardPlatform.Domain.Payments;
using JobBoardPlatform.Domain.Provinces;
using JobBoardPlatform.Domain.Users;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IUserREpository UserRepo { get; }
        ICompanyRepository CompanyRepo { get; }
        ICityRepository CityRepo { get; }
        IProvinceRepository ProvinceRepo { get; }
        IJobAdRepositpry JobAdsRepo { get; }
        IJobCategoryRepository JobCategoryRepo { get; }
        INotificationRepo NotificationsRepo { get; }
        IPaymentRepository PaymentsRepo { get; }
        IApplicationREpository ApplicationRepo { get; }
        IAttachRepo AttacheRepo { get; }
        public IRefreshTokenRepo RefreshTokenRepo { get; }
        UserManager<User> userManager { get; }
         RoleManager<RoleEntity> roleManager { get; }

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();

        Task SaveChangesAsync();
    }


}
