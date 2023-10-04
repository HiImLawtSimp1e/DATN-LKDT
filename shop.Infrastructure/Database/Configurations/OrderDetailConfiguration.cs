using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace shop.Infrastructure.Database.Configurations
{
    public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasKey(od => new { od.OrderId, od.ProductDetailId });
            builder.Property(od => od.Quantity).IsRequired();
            builder.Property(od => od.Price).IsRequired();
            builder.Property(od => od.Status).IsRequired();

            builder.HasOne(od => od.Order).WithMany(o => o.OrderDetails).HasForeignKey(od => od.OrderId);

            builder.HasOne(od => od.ProductDetail).WithMany(pd => pd.OrderDetails).HasForeignKey(od => od.ProductDetailId);

        }
    }
}
