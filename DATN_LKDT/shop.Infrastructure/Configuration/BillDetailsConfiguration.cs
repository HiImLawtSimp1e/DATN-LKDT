using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;
namespace AppData.Configuration
{
    public class BillDetailsConfiguration : IEntityTypeConfiguration<BillDetailsEntity>
    {
        public void Configure(EntityTypeBuilder<BillDetailsEntity> builder)
        {
            builder.HasKey(bd => bd.Id);

            builder.HasOne(bd => bd.Bills)
                .WithMany(b => b.BillDetails)
                .HasForeignKey(bd => bd.BillId);

            builder.HasOne(bd => bd.Discounts)
                .WithMany(d => d.BillDetails)
                .HasForeignKey(bd => bd.DiscountId);
        }
    }
}
