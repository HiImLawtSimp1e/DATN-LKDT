using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;
namespace AppData.Configuration
{
    public class WarrantyCardConfiguration : IEntityTypeConfiguration<WarrantyCardEntity>
    {
        public void Configure(EntityTypeBuilder<WarrantyCardEntity> builder)
        {
            builder.HasKey(p => p.Id);

            
        }
    }
}
