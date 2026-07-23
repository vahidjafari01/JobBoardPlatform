using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record  CreateAppCommand
    {
        public CreateAppCommand(Guid userId, Guid requesterId, Guid jobAdID, string? note)
        {
            UserId = userId;
            RequesterId = requesterId;
            this.jobAdID = jobAdID;
            Note = note;
        }

        public Guid UserId { get; set; }
        public Guid RequesterId { get; set; }
        public Guid jobAdID { get; set; }
        public string? Note { get; set; }
    }
}
