using JobBoardPlatform.Domain.Users;
using JobBoardPlatform.Presentation.Dtos;
using Microsoft.AspNetCore.Identity;

namespace JobBoardPlatform.Presentation
{
    public static class SeedData
    {
        public static async Task SeedDataBaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            await SeedAdminsAsync(scope.ServiceProvider);
        }
        private static async Task SeedAdminsAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var adminData = configuration.GetSection("AdminData").Get<AdminData>();

            if (adminData is null) return;


            if (adminData != null)
            {
                var adminUser = new User { UserName = adminData.Username,FirstName = adminData.FirstName,LastName = adminData.LastName,Email = adminData.Email};
               var a = await userManager.CreateAsync(adminUser, adminData.Password);
               var b = await userManager.AddToRoleAsync(adminUser,"Admin");
            }
        }
    }
}
