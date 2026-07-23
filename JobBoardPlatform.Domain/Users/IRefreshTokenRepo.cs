using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Users
{
    public interface IRefreshTokenRepo:IGenericRepository<RefreshToken>
    {
        Task<RefreshToken?> GetTokenByRefreshTokenAsync(string token);
    }
}
