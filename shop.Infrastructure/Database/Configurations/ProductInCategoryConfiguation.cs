using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace shop.Infrastructure.Database.Configurations
{
    public class ProductInCategoryConfiguation : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(pic => new { pic.ProductId, pic.CategoryId });

            builder.HasOne(pic => pic.Product).WithMany(p => p.ProductInCategories).HasForeignKey(pic => pic.ProductId);

            builder.HasOne(pic => pic.Category).WithMany(c => c.ProductInCategories).HasForeignKey(pic => pic.CategoryId);
        }
    }
}
