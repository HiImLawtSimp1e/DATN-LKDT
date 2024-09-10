using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using shop.Domain.Entities;
using shop.Infrastructure.Initialization;
using System.Reflection;
using System.Reflection.Emit;

namespace shop.Infrastructure.Database.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public AppDbContext()
    {
    }

    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<RoleEntity> Roles {  get; set; }
    public DbSet<AddressEntity> Address { get; set; }
    public DbSet<BlogEntity> Blogs { get; set; }
    public DbSet<CartEntity> Carts { get; set; }
    public DbSet<DiscountEntity> Discounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<ProductValue> ProductValues { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<VnpayTransactions> VnpayTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<CartItem>()
           .HasKey(ci => new { ci.CartId, ci.ProductId, ci.ProductTypeId });

        builder.Entity<OrderItem>()
            .HasKey(oi => new { oi.OrderId, oi.ProductId, oi.ProductTypeId });

        builder.Entity<Order>(entity =>
        {
            entity.Property(o => o.State)
                  .HasConversion<int>()
                  .IsRequired();
        });

        Seeding.SeedingAccount(builder);
        Seeding.SeedingBase(builder);
        Seeding.SeedingData(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
