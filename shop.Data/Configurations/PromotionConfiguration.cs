using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Data.Configurations;

public class PromotionConfiguration : IEntityTypeConfiguration<Promotion>
{
    public void Configure(EntityTypeBuilder<Promotion> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p=> p.PromotionCode).IsRequired();
        builder.Property(p=> p.StartedDate).IsRequired();
        builder.Property(p=> p.FinishedDate).IsRequired();
        builder.Property(p=> p.DiscountAmount).IsRequired();
        builder.Property(p=> p.DiscountPercent).IsRequired();
        builder.Property(p=> p.Status).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();


    }
}
