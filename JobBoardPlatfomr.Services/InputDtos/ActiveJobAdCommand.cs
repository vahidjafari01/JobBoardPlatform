using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record ActiveJobAdCommand
    {
        public Guid RequesterId { get; set; }
        public Guid JobId { get; set; }
    }
    public record ArchiveMyJobAdCommand{
        public Guid RequesterId { get; set; }
        public Guid JobId { get; set; }

    }
}
