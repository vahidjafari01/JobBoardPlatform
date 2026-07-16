using JobBoardPlatfomr.Services.IServices;
using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.Services
{
    public class JwtService : IJwtService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration config, IUnitOfWork unitOfWork)
        {
            _configuration = config;
            this.unitOfWork = unitOfWork;
        }

        public async Task<string> GenerateTokenAsync(User user, IList<string> Roles)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName !),

                new (JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new (JwtRegisteredClaimNames.Email, user.Email!),
            };


            foreach (var role in Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
                if(role == "Employer")
                {
                    if(await IsApproved(user.Id))
                    {
                        claims.Add(new Claim("IsApproved","true"));
                    }
                }
            }
            
            


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

   

        private async Task<bool> IsApproved(Guid userId)
        {
            var company =await unitOfWork.CompanyRepo.GetByUserId(userId);
            if (company == null) {
             return false;
            }
            else
            {
                return company.IsApproved;
            }
        }
    }
}
