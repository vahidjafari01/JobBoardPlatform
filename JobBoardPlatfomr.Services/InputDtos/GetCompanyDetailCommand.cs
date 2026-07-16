using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.InputDtos
{
    public record GetCompanyDetailCommand
    {
        public Guid CompanyId{ get; set; }
        public Guid RequesterId{ get; set; }
    }
}
