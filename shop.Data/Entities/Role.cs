namespace shop.Data.Entities;
public class Role
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }

    // relationship
    public virtual ICollection<Staff> Staffs { get; set; }
}
