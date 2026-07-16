using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Notifications
{
    public class Notification:BaseEntity
    {
        public Notification(string message, string email, string phoneNumber)
        {
            Message = message;
            Email = email;
            PhoneNumber = phoneNumber;
        }
        public Notification()
        {
            
        }


        public string Message { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
