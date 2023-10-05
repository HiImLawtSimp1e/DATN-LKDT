using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace shop.Infrastructure.Database.Configurations
{
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(cd => cd.Id);
            builder.Property(cd => cd.Quantity).IsRequired();
            builder.Property(cd => cd.Price).IsRequired();

            builder.HasOne(cd => cd.Cart).WithMany(c => c.CartDetails).HasForeignKey(cd => cd.CustomerId);

            builder.HasOne(cd => cd.ProductDetail).WithMany(pd => pd.CartDetails).HasForeignKey(cd => cd.ProductDetailId);

        }
    }
}
