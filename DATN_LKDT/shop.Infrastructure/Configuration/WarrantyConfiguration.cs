using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;
namespace AppData.Configuration
{
    public class WarrantyConfiguration : IEntityTypeConfiguration<WarrantyEntity>
    {
        public void Configure(EntityTypeBuilder<WarrantyEntity> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
