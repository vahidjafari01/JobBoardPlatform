using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Abstractions
{
    public class Attach : BaseEntity
    {
        private Attach()  //ef
        {

        }
        public Attach(byte[] filedb64, string contentType, string fileName)
        {
            Filedb64 = filedb64;
            ContentType = contentType;
            FileName = fileName;
        }
        public Byte[] Filedb64 { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
    }
}
