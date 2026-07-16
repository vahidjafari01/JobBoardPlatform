using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.Applications;
using JobBoardPlatform.Domain.Companies;
using JobBoardPlatform.Domain.Payments;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Users
{
    public class User:IdentityUser<Guid>,IEntity
    {
        public User()
        {
            
        }
        public User(Guid? resumeId)
        {
            CreatedAt = DateTime.UtcNow;
            ModifiedAt = DateTime.UtcNow;
            IsDeleted = false;
            ResumeId = resumeId;
        }
        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string LastName{ get; set; }
        public DateTime CreatedAt { get ; set ; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get ; set ; }
        public bool IsDeleted { get; set; } = false;
        public Guid? ProfilePhotoId{ get; set; }
        public Guid? ResumeId{ get; set; }

        public Attach? ProfilePhoto{ get; set; }

        public Company? Company { get; set; }
        public List<Application>? Applications{ get; set; }
    }
}
