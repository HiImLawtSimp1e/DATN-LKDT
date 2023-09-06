namespace shop.Data.Entities;
public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    // relationship
    public Guid? ParentId { get; set; }
    public virtual ICollection<ProductInCategory> ProductInCategories { get; set; }
}
