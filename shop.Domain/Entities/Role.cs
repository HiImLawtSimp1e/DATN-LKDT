using shop.Domain.Entities.Base;

namespace shop.Domain.Entities;
public class Role : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Status { get; set; }
    public virtual ICollection<Staff>? Staffs { get; set; }
}
