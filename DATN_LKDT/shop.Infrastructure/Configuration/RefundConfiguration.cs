using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;
namespace AppData.Configuration
{
    public class RefundConfiguration : IEntityTypeConfiguration<RefundEntity>
    {
        public void Configure(EntityTypeBuilder<RefundEntity> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
