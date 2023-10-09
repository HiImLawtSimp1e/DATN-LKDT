using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace shop.Infrastructure.Database.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.OrderCode).IsRequired();
            builder.Property(o => o.CreatedDate).IsRequired();
            builder.Property(o => o.ConfirmedDate).IsRequired();
            builder.Property(o => o.PaidDate).IsRequired();
            builder.Property(o => o.ShipDate).IsRequired();
            builder.Property(o => o.CompletedDate).IsRequired();
            builder.Property(o => o.ShipName).IsRequired();
            builder.Property(o => o.ShipAddress).IsRequired();
            builder.Property(o => o.ShipPhoneNumber).IsRequired();
            builder.Property(o => o.Total).IsRequired();
            builder.Property(o => o.Status).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();

            builder.HasOne(o => o.Voucher).WithMany(v => v.Orders).HasForeignKey(o => o.VoucherId);
            builder.HasOne(o => o.Staff).WithMany(s => s.Orders).HasForeignKey(o => o.StaffId);

            
        }
    }
}
