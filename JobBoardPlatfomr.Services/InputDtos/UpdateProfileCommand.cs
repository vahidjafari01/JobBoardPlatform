using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record UpdateProfileCommand
    {
        public UpdateProfileCommand(string firsName, string lastname, string userName, string email, Guid requester, Guid userId)
        {
            FirsName = firsName;
            this.lastname = lastname;
            UserName = userName;
            Email = email;
            Requester = requester;
            UserId = userId;
        }

        public string FirsName { get; set; }
        public string lastname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid Requester { get; set; }
        public Guid UserId { get; set; }
    }
}
