using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record ApplicationJobAdCommand
    {

        public Guid RequesterId { get; set; }
        public Guid JobAdId { get; set; }
    }
}
