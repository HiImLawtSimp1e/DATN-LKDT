using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class Product : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public virtual ICollection<ProductDetail>? ProductDetails { get; set; }
    public virtual ICollection<ProductInCategory>? ProductInCategories { get; set; }
}
