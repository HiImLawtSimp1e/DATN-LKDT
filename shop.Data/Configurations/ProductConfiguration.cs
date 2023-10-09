using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Data.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x=>x.Id);
        builder.Property(x=>x.Name).IsRequired();
        builder.Property(x=>x.Description).IsRequired();
        builder.Property(x=>x.Status).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();
    }
}
