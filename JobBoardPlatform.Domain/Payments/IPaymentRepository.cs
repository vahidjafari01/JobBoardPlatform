using JobBoardPlatform.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Payments
{
    public interface IPaymentRepository:IGenericRepository<Payment>
    {
    }
}
