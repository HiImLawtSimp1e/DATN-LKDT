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
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            builder.HasKey(cd => new {cd.ProductDetailId, cd.CustomerId });
            builder.Property(cd=>cd.Quantity).IsRequired();
            builder.Property(cd=>cd.Price).IsRequired();

            builder.HasOne(cd => cd.Cart).WithMany(c => c.CartDetails).HasForeignKey(cd => cd.CustomerId);

            builder.HasOne(cd => cd.ProductDetail).WithMany(pd => pd.CartDetails).HasForeignKey(cd => cd.ProductDetailId);

        }
    }
}
