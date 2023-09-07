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
    public class ProductInCategoryConfiguation : IEntityTypeConfiguration<ProductInCategory>
    {
        public void Configure(EntityTypeBuilder<ProductInCategory> builder)
        {
            builder.HasKey(pic => new {pic.ProductId, pic.CategoryId});

            builder.HasOne(pic => pic.Product).WithMany(p => p.ProductInCategories).HasForeignKey(pic => pic.ProductId);

            builder.HasOne(pic => pic.Category).WithMany(c => c.ProductInCategories).HasForeignKey(pic => pic.CategoryId);
        }
    }
}
