using JobBoardPlatform.Domain.Abstractions;
using JobBoardPlatform.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Domain.Payments
{
    public class Payment:BaseEntity
    {
        public Payment(Guid jobAdId,  decimal amount,PaymentStatus status, string? provider, string? transactionReference, DateTime? paidAt)
        {
            JobAdId = jobAdId;
            Amount = amount;
            Status = status;
            Provider = provider;
            TransactionReference = transactionReference;
            validate();
        }

        public Guid JobAdId { get; set; }
        public JobAd JobAd { get; set; }

        public decimal Amount { get; set; }

        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public string? Provider { get; set; }              
        public string? TransactionReference { get; set; }  
        public DateTime? PaidAt { get; set; }
        private void validate()
        {
            if(Amount < 0)
            {
                throw new ArgumentOutOfRangeException("amount of payment can not be zero or neggative");
            }
        }


    }

}
