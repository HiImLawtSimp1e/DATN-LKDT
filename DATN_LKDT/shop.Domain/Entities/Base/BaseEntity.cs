using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities.Base;

[NotMapped]
public class BaseEntity
{
    public virtual string CreatedBy { get; set; } = string.Empty;

    public virtual string ModifiedBy { get; set; } = string.Empty;

    public virtual DateTime ModifiedAt { get; set; } = DateTime.Now;

    public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
}
