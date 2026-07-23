using JobBoardPlatform.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class RefreshTokenRepo : GenericRepository<RefreshToken>, IRefreshTokenRepo
    {
        public RefreshTokenRepo(AppDbContext context) : base(context)
        {
        }

        public async Task<RefreshToken?> GetTokenByRefreshTokenAsync(string token)
        {
            return await _context.RefreshTokens.Include(r => r.User).FirstOrDefaultAsync(r => r.Token == token);
        }

        
    }
}
