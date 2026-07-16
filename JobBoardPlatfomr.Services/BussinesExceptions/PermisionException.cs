using JobBoardPlatform.Domain.BaseExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatfomr.Services.BussinesExceptions
{
    public class PermisionException : BaseException
    {
        public PermisionException(string message, string code, Exception? innerException = null) : base(message, code, innerException)
        {
        }
    }
}
