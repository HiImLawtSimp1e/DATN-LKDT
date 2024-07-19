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

    public DbSet<ContactEntity> Contact { get; set; }
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<AddressEntity> Address { get; set; }
    public DbSet<BillEntity> Bill { get; set; }
    public DbSet<BillDetailsEntity> BillDetails { get; set; }
    public DbSet<BlogEntity> Blogs { get; set; }
    public DbSet<CartEntity> Carts { get; set; }
    public DbSet<CartDetailsEntity> CartDetails { get; set; }
    public DbSet<DiscountEntity> Discount { get; set; }
    public DbSet<ListImageEntity> ListImage { get; set; }
    public DbSet<ProductionCompanyEntity> ProductionCompany { get; set; }
    public DbSet<RankEntity> Ranks { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<WarrantyEntity> Warranty { get; set; }
    public DbSet<WarrantyCardEntity> WarrantyCards { get; set; }
    public DbSet<SalesEntity> Sales { get; set; }
    public DbSet<SaleDetaildEntity> SalePhoneDetailds { get; set; }
    public DbSet<VirtualItemObjRelationEntity> ItemObjRelationEntities { get; set; }
    public DbSet<ApplicationUser> AspNetUsers { get; set; }
    public DbSet<ApplicationRole> AspNetRoles { get; set; }
    public DbSet<VirtualItemEntity> VirtualItems { get; set; }
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

        builder.Entity<VirtualItemEntity>().HasKey(p => p.Id);
        //builder.Entity<VW_Phone>().ToView("VW_Phone").HasNoKey();
        //builder.Entity<VW_PhoneDetail>().ToView("VW_PhoneDetail").HasNoKey();
        //builder.Entity<VW_Phone_Group>().ToView("VW_Phone_Group").HasNoKey();
        //builder.Entity<VW_List_By_IdPhone>().ToView("VW_List_By_IdPhone").HasNoKey();
        //builder.Entity<VTop5_PhoneSell>().ToView("VTop5_PhoneSell").HasNoKey();
        //builder.Entity<vOverView>().ToView("vOverView").HasNoKey();
        //builder.Entity<BillGanDay>().ToView("BillGanDay").HasNoKey();
        //builder.Entity<PhoneStatitics>().ToView("PhoneStatitics").HasNoKey();

        Seeding.SeedingAccount(builder);
        Seeding.SeedingData(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
