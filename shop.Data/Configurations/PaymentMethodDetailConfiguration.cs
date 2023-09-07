using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Data.Configurations
{
    public class PaymentMethodDetailConfiguration : IEntityTypeConfiguration<PaymentMethodDetail>
    {
        public void Configure(EntityTypeBuilder<PaymentMethodDetail> builder)
        {
            builder.HasKey(pmd => pmd.Id);
            builder.Property(pmd => pmd.Amount).IsRequired();
            builder.Property(pmd => pmd.Status).IsRequired();

            builder.HasOne(pmd=>pmd.PaymentMethod).WithMany(pm=>pm.PaymentMethodDetails).HasForeignKey(pmd=>pmd.PaymentMethodId);
            builder.HasOne(pmd=>pmd.Order).WithMany(o=>o.PaymentMethodDetails).HasForeignKey(pmd=>pmd.OrderId);

        }
    }
}
