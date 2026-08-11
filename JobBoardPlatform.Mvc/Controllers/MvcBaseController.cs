using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JobBoardPlatform.Mvc.Controllers
{
    public abstract class MvcBaseController : Controller
    {
        protected const string CookieScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        protected Guid CurrentUserId =>
            Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : Guid.Empty;

        protected async Task SignInUserAsync(User user, IUnitOfWork unitOfWork)
        {
            var roles = await unitOfWork.userManager.GetRolesAsync(user);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                if (role == "Employer")
                {
                    var company = await unitOfWork.CompanyRepo.GetByUserId(user.Id);
                    if (company != null && company.IsApproved)
                        claims.Add(new Claim("IsApproved", "true"));
                }
            }

            if (user.IsActive)
                claims.Add(new Claim("IsActive", "true"));

            var identity = new ClaimsIdentity(claims, CookieScheme);
            await HttpContext.SignInAsync(CookieScheme, new ClaimsPrincipal(identity));
        }
    }
}
