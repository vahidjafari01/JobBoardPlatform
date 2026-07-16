using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record GetJObAdDetailCommand
    {
        public GetJObAdDetailCommand(Guid requesterId, Guid jobAdId)
        {
            RequesterId = requesterId;
            JobAdId = jobAdId;
        }

        public Guid RequesterId { get; set; }
        public Guid JobAdId { get; set; }
    }
}
