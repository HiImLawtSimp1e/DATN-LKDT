namespace shop.Data.Entities;
public class ProductInCategory
{
    // relationship
    public Guid CategoryId { get; set; }
    public Guid ProductId { get; set; }
    public virtual Category Category { get; set; }
    public virtual Product Product { get; set; }
}
