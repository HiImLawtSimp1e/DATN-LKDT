using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;
namespace AppData.Configuration
{
    public class ListImageConfiguration : IEntityTypeConfiguration<ListImageEntity>
    {
        public void Configure(EntityTypeBuilder<ListImageEntity> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
