using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class Size : BaseEntity
{
    public Guid Id { get; set; }
    public int SizeNumber { get; set; }
    public virtual ICollection<ProductDetail>? ProductDetails { get; set; }
}
