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
    public class UsePointHistoryConfiguration : IEntityTypeConfiguration<UsePointHistory>
    {
        public void Configure(EntityTypeBuilder<UsePointHistory> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u=>u.UsedPoint).IsRequired();
            builder.Property(u=>u.Status).IsRequired();

            builder.HasOne(u=>u.Customer).WithMany(c => c.UsePointHistories).HasForeignKey(u=>u.CustomerId);
            builder.HasOne(u=>u.ExchangePoint).WithMany(e => e.UsePointHistories).HasForeignKey(u=>u.ExchangePointId);
            builder.HasOne(u=>u.Order).WithMany(o => o.UsePointsHistories).HasForeignKey(u=>u.OrderId);
        }
    }
}
