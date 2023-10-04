using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace shop.Infrastructure.Database.Configurations
{
    public class ExchangePointConfiguration : IEntityTypeConfiguration<ExchangePoint>
    {
        public void Configure(EntityTypeBuilder<ExchangePoint> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Status).IsRequired();
            builder.Property(e => e.Point).IsRequired();
            builder.Property(e => e.AddPointRatio).IsRequired();
            builder.Property(e => e.UsePointHistories).IsRequired();
        }
    }
}
