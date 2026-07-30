using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Users
{
    public record UserDto
    {
        public UserDto(Guid userId, string username, string email)
        {
            UserId = userId;
            Username = username;
            Email = email;
        }

        private UserDto() { }
        

            public Guid UserId{ get; set; }
            public string Username { get; set; }
            public string Email { get; set; }

        }




    }

