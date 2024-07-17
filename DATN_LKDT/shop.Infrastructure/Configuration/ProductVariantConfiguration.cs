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
    public class ProductVariantConfiguration : IEntityTypeConfiguration<ProductVariant>
    {
        public void Configure(EntityTypeBuilder<ProductVariant> builder)
        {
            builder.HasKey(v => new { v.ProductId, v.ProductTypeId });
            builder.HasOne(v => v.Product)
                .WithMany(p => p.ProductVariants)
                .HasForeignKey(p => p.ProductId);
            builder.HasOne(v => v.ProductType)
                .WithMany(pt => pt.ProductVariants);
        }
    }
}
