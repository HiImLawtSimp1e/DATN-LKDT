using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace shop.Infrastructure.Database.Configurations
{
    public class ProductDetailConfiguration : IEntityTypeConfiguration<ProductDetail>
    {
        public void Configure(EntityTypeBuilder<ProductDetail> builder)
        {
            builder.HasKey(pd => pd.Id);
            builder.Property(pd => pd.Status).IsRequired();
            builder.Property(pd => pd.Stock).IsRequired();
            builder.Property(pd => pd.OriginalPrice).IsRequired();
            builder.Property(pd => pd.Price).IsRequired();
            builder.Property(pd => pd.CreatedDate).IsRequired();

            builder.HasOne(pd => pd.Product).WithMany(p => p.ProductDetails).HasForeignKey(pd => pd.ProductId);
            builder.HasOne(pd => pd.Color).WithMany(c => c.ProductDetails).HasForeignKey(pd => pd.ColorId);
            builder.HasOne(pd => pd.Size).WithMany(s => s.ProductDetails).HasForeignKey(pd => pd.SizeId);
            builder.HasOne(pd => pd.Promotion).WithMany(pr => pr.ProductDetails).HasForeignKey(pd => pd.PromotionId);
        }
    }
}
