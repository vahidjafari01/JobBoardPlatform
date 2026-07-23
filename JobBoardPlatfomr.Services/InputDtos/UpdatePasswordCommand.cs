using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record UpdatePasswordCommand
    {
        public UpdatePasswordCommand(Guid requester, Guid userId, string currentPassword, string newPassword)
        {
            Requester = requester;
            UserId = userId;
            CurrentPassword = currentPassword;
            NewPassword = newPassword;
        }

        public Guid Requester { get; set; }
        public Guid UserId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

}
