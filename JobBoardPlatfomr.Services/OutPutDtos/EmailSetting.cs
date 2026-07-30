using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.OutPutDtos
{
    public class EmailSettings
    {

        public const string SectionName = "EmailSettings";

        public string Host { get; set; }

        public int Port { get; set; }

        public string UserName { get; set; } //Email

        public string Password { get; set; } // App Password

        public string FromName { get; set; }
        public string FromEmail { get; set; }

        public bool UseSsl { get; set; }

        public bool DefaultHtml { get; set; }
    }
}
