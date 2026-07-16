using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record DetailAppDto
    {
        public DetailAppDto(Guid appId, Attach? resume, string? phoneNumber, string? email, string? note, string firstName, string lastName, DateTime submitedAt, string status)
        {
            AppId = appId;
            this.resume = resume;
            PhoneNumber = phoneNumber;
            Email = email;
            Note = note;
            FirstName = firstName;
            LastName = lastName;
            SubmitedAt = submitedAt;
            this.status = status;
        }
        public DetailAppDto()
        {
            
        }
        public Guid AppId { get; set; }
        public Attach? resume { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime SubmitedAt { get; set; }
        public string status { get; set; }


    }
}
