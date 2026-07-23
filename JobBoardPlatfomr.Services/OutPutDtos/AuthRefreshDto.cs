using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record AuthRefreshDto
    {
        public string AccessToken { get; set; }

        public AuthRefreshDto(string accessToken)
        {
            AccessToken = accessToken;
        }
    }



    
}
