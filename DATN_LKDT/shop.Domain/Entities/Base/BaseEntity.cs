using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities.Base;

[NotMapped]
public class BaseEntity
{
    public virtual Guid? CreatedByUserId { get; set; }

    public virtual Guid? LastModifiedByUserId { get; set; }

    public virtual DateTime ModifiedAt { get; set; } = DateTime.Now;

    public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
}
