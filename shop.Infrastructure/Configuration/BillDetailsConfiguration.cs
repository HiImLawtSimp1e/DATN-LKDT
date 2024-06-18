using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;
namespace AppData.Configuration
{
    public class BillDetailsConfiguration : IEntityTypeConfiguration<BillDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<BillDetailsEntity> builder)
        {
            builder.HasKey(p => p.Id);


            builder.HasOne(p => p.Discounts).WithMany().HasForeignKey(p => p.IdDiscount);

        }
    }
}
