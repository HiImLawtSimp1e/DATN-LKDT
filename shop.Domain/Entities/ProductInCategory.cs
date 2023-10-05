using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class ProductInCategory : BaseEntity
{
    public Guid Id { get; set; }
    public Guid CategoryId { get; set; }
    public Guid ProductId { get; set; }
    public virtual Category? Category { get; set; }
    public virtual Product? Product { get; set; }
}
