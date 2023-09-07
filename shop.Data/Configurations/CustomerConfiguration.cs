using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Data.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c=> c.FirstName).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired();
            builder.Property(c => c.LastName).HasColumnType("nvarchar(200)").HasMaxLength(200).IsRequired();
            builder.Property(c=>c.Address).IsRequired();
            builder.Property(c=>c.DateOfBirth).IsRequired();
            builder.Property(c=>c.Gender).IsRequired();
            builder.Property(c => c.UserName).HasColumnType("varchar(200)").IsRequired();
            builder.Property(c => c.Email).HasColumnType("varchar(320)").IsRequired();
            builder.Property(c=>c.EmailComfirmed);
            builder.Property(c=>c.PhoneNumber).IsRequired();
            builder.Property(c=>c.PhoneNumberConfirmed);
            builder.Property(c=>c.PasswordHash).IsRequired();
            builder.Property(c=>c.Point);
            builder.Property(c=>c.Status).IsRequired();

        }
    }
}
