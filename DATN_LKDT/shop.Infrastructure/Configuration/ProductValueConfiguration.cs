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
    public class ProductValueConfiguration : IEntityTypeConfiguration<ProductValue>
    {
        public void Configure(EntityTypeBuilder<ProductValue> builder)
        {
            builder.HasKey(pv => new { pv.ProductId, pv.ProductAttributeId });
            builder.HasOne(pv => pv.Product)
                .WithMany(p => p.ProductValues)
                .HasForeignKey(pv => pv.ProductId);
            builder.HasOne(pv => pv.ProductAttribute)
                .WithMany(pa => pa.ProductValues)
                .HasForeignKey(pv => pv.ProductAttributeId);
        }
    }
}
