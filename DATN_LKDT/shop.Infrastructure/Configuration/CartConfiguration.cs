using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace AppData.Configuration
{
    public class CartConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(c => c.Accounts)
              .WithOne(a => a.Cart)
              .HasForeignKey<CartEntity>(c => c.Id);

            builder.HasMany(c => c.CartItems)
               .WithOne(ci => ci.Cart)
               .HasForeignKey(ci => ci.CartId);
        }
    }
}
