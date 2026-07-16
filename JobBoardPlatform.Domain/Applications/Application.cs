using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.entities;
using JobBoardPlatform.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Applications
{
    public class Application:BaseEntity
    {
        public Application()
        {
            
        }
        public Application(ApplicationStatus status, Guid jobAdId, Guid userId)
        {
            Status = status;
            JobAdId = jobAdId;
            UserId = userId;
        }
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Submitted;

        public DateTime? ReviewedAt { get; set; }

        public DateTime? InterviewAt { get; set; }

        public string? NoteWritenByUser{ get; set; }

        public Guid JobAdId{ get; set; }

        public JobAd JObAd{ get; set; }


        public Guid UserId{ get; set; }
        public User User{ get; set; }
    }

}
