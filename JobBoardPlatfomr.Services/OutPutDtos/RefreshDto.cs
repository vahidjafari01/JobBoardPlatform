using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public record RefreshDto
    {

        public string token { get; set; }
        public Guid UserId { get; set; }

        public RefreshDto(string token, Guid userId)
        {
            this.token = token;
            UserId = userId;
        }
    }
}
