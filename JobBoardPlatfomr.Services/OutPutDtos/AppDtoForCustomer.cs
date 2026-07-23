using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record AppDtoForCustomer
    {
        public AppDtoForCustomer(Guid appId, string companyName, string jobAdTitle, Guid jobAdId)
        {
            AppId = appId;
            CompanyName = companyName;
            JobAdTitle = jobAdTitle;
            JobAdId = jobAdId;
        }

        public Guid AppId { get; set; }
        public Guid JobAdId { get; set; }
        public string CompanyName { get; set; }
        public string JobAdTitle { get; set; }
    }
}
