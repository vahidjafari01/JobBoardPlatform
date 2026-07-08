using JobBoardPlatform.Domain.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Users
{
    public class RoleEntity : IdentityRole<Guid>, IEntity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get ; set ; }
    }
}
