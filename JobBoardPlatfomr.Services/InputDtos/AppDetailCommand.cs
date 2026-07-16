using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record AppDetailCommand
    {
        public Guid RequesterId { get; set; }
        public Guid AppId { get; set; }
    }
}
