using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public class AttachOutputDto
    {
        public string Filename { get; set; }
        public string contentType { get; set; }
        public byte[] Filedb64 { get; set; }
    }
}
