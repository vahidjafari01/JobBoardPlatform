using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Users
{
    public class RefreshToken:BaseEntity
    {
        private RefreshToken()//ef
        {
            
        }
        public RefreshToken(string token, Guid userId, DateTime expiredAt)
        {
            Token = token;
            UserId = userId;
            ExpiredAt = expiredAt;
            CreatedAt = DateTime.UtcNow;
        }

        public string Token { get; set; }
        public Guid UserId { get; set; }
        public DateTime ExpiredAt{ get; set; }
        public bool IsActive {  get; set; } = true;
        public User User { get; set; }


    }
}
