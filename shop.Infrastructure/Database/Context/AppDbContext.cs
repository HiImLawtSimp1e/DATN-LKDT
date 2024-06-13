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
    public DbSet<VirtualItemEntity> VirtualItems { get; set; }
}
