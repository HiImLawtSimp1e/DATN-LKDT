using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class OrderDetail : BaseEntity
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public int Status { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductDetailId { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual Order? Order { get; set; }
    public virtual ProductDetail? ProductDetail { get; set; }
}
