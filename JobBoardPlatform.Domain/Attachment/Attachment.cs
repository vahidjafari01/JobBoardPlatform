using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Attachment
{
    public class Attachment:BaseEntity
    {
        public Attachment()
        {
            
        }
        public Attachment(string filedb64)
        {
            Filedb64 = filedb64;
        }
        public string Filedb64 { get; set; }
    }
}
