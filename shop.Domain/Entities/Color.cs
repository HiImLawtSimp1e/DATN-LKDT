using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class Color : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual ICollection<ProductDetail>? ProductDetails { get; set; }
}
