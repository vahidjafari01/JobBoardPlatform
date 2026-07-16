using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record GetMyJobAdsCommand
    {
        public GetMyJobAdsCommand(Guid requesterId, Guid companyId)
        {
            RequesterId = requesterId;
            this.companyId = companyId;
        }

        public Guid RequesterId{ get; set; }
        public Guid companyId {  get; set; }
    }
}
