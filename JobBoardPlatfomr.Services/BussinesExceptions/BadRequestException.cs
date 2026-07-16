using JobBoardPlatform.Domain.BaseExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.BussinesExceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException(string message,Exception? innerException = null) : base(message, "400", innerException)
        {
        }
    }
}
