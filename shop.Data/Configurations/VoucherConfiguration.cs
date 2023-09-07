using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace shop.Data.Configurations
{
    public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
    {
        public void Configure(EntityTypeBuilder<Voucher> builder)
        {
            builder.HasKey(v=>v.Id);
            builder.Property(v=>v.VoucherCode).IsRequired();
            builder.Property(v=>v.Name).IsRequired();
            builder.Property(v=>v.Amount).IsRequired();
            builder.Property(v=>v.StartedDate).IsRequired();
            builder.Property(v=>v.FinishedDate).IsRequired();
            builder.Property(v=>v.Stock).IsRequired();
            builder.Property(v=>v.Description).IsRequired();
            builder.Property(v=>v.Status).IsRequired();
            
            
        }
    }
}
