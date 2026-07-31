using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.IServices
{
    public interface IEmailSender
    {
        Task SendAsync(Guid userId, string subject, string body);
    }
}
