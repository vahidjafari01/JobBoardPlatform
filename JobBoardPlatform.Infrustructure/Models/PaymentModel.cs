using JobBoardPlatform.Domain.Payments;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobBoardPlatform.Infrustructure.Models
{
    public class PaymentModel : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.Property(a => a.Amount).HasPrecision(10, 2);
            builder.Property(a => a.TransactionReference).HasColumnType("nvarchar(50)").HasMaxLength(50);
            builder.Property(a => a.Provider).HasColumnType("nvarchar(50)").HasMaxLength(50);

        }
    }
}
