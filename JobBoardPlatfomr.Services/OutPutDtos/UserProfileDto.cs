using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record UserProfileDto
    {
        public UserProfileDto(string username, string email, string firstName, string lastName, DateTime createdAt, Guid? resumeId, bool isActive)
        {
            Username = username;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            CreatedAt = createdAt;
            ResumeId = resumeId;
            IsActive = isActive;
        }

        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive{ get; set; }
        public DateTime CreatedAt{ get; set; }
        public Guid? ResumeId{ get; set; }
      
    }
}
