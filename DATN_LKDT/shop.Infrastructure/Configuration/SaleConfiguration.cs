using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace AppData.Configuration
{
    public class SaleConfiguration : IEntityTypeConfiguration<SalesEntity>
    {
        public void Configure(EntityTypeBuilder<SalesEntity> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
