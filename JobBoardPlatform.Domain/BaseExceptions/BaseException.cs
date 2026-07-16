using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.BaseExceptions
{
    public class BaseException : Exception
    {
        public BaseException(string message, string code, Exception? innerException = null)
            : base(message, innerException)
        {
            Code = code;
        }
        public string Code { get; private set; }
    }
}
