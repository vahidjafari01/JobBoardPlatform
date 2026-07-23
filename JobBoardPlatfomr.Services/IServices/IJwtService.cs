using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IJwtService
    {
        Task<string> GenerateTokenAsync(User user, IList<string> Roles);
        Task<string> GenerateRefreshToken(Guid userId);
    }
}
