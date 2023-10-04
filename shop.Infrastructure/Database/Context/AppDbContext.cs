using Microsoft.EntityFrameworkCore;
using shop.Domain.Entities;

namespace shop.Infrastructure.Database.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

    public AppDbContext()
    {
    }

    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartDetail> CartDetails { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<ExchangePoint> ExchangePoints { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<PaymentMethodDetail> PaymentMethodDetails { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductDetail> ProductDetails { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductInCategory> ProductInCategories { get; set; }
    public DbSet<Promotion> Promotions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Staff> Staffs { get; set; }
    public DbSet<UsePointHistory> UsePointsHistories { get; set; }
    public DbSet<Voucher> Vouchers { get; set; }
}
