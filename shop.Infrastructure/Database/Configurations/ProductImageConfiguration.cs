using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace shop.Infrastructure.Database.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Caption).IsRequired();
            builder.Property(x => x.ImagePath).IsRequired();
            builder.Property(x => x.IsDefault).IsRequired();
            builder.Property(x => x.SortOrder).IsRequired();

        }
    }
}
