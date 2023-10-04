using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Domain.Entities;

namespace shop.Infrastructure.Database.Configurations
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.VoucherCode).IsRequired();
            builder.Property(v => v.Name).IsRequired();
            builder.Property(v => v.Amount).IsRequired();
            builder.Property(v => v.StartedDate).IsRequired();
            builder.Property(v => v.FinishedDate).IsRequired();
            builder.Property(v => v.Stock).IsRequired();
            builder.Property(v => v.Description).IsRequired();
            builder.Property(v => v.Status).IsRequired();


        }
    }
}
