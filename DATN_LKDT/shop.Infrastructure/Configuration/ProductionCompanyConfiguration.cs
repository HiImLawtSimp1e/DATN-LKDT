using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;
namespace AppData.Configuration
{
    public class ProductionCompanyConfiguration : IEntityTypeConfiguration<ProductionCompanyEntity>
    {
        public void Configure(EntityTypeBuilder<ProductionCompanyEntity> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
