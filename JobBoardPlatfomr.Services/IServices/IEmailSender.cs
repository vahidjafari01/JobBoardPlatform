using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IEmailSender
    {
        public Task SendAsync(string to, string subject, string body, bool isHtml, CancellationToken cancellationToken);
    }
}
