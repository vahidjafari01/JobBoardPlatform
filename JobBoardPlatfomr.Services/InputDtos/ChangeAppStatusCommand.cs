using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record ChangeAppStatusCommand
    {
        public ChangeAppStatusCommand(Guid requesterId, Guid appId, string status)
        {
            RequesterId = requesterId;
            AppId = appId;
            this.status = status;
        }

        public Guid RequesterId { get; set; }
        public Guid AppId { get; set; }
        public string status { get; set; }
    }
}
