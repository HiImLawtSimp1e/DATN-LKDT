using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class Cart : BaseEntity
{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual Customer? Customer { get; set; }
    public virtual ICollection<CartDetail>? CartDetails { get; set; }
}
