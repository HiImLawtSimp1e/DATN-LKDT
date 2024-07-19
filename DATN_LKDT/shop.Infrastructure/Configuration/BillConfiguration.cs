using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;
namespace AppData.Configuration
{
    public class BillConfiguration : IEntityTypeConfiguration<BillEntity>
    {
        public void Configure(EntityTypeBuilder<BillEntity> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Accounts).WithMany().HasForeignKey(p => p.IdAccount);

            builder.HasMany(c => c.BillDetails)
             .WithOne(cd => cd.Bills)
             .HasForeignKey(cd => cd.Id);
        }
    }
}
