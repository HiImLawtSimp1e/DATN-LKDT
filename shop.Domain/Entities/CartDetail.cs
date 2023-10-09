using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class CartDetail : BaseEntity
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid CustomerId { get; set; }
    public Guid ProductDetailId { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual Cart? Cart { get; set; }
    public virtual ProductDetail? ProductDetail { get; set; }
}
