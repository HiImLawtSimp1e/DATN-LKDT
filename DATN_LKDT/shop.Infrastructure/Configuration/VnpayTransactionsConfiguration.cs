using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Configuration
{
    public class VnpayTransactionsConfiguration : IEntityTypeConfiguration<VnpayTransactions>
    {
        public void Configure(EntityTypeBuilder<VnpayTransactions> builder)
        {
            builder.HasKey(trans => trans.TransactionId);
        }
    }
}
