using JobBoardPlatform.Domain.Payments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Repositories
{
    public class PaymentRepo : GenericRepository<Payment>, IPaymentRepository
    {
        public PaymentRepo(AppDbContext context) : base(context)
        {
        }
    }
}
