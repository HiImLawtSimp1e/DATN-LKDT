using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities.Base;

[NotMapped]
public class BaseEntity
{
    public virtual Guid? CreatedByUserId { get; set; }

    public virtual Guid? LastModifiedByUserId { get; set; }

    public virtual DateTime LastModifiedOnDate { get; set; } = DateTime.Now;

    public virtual DateTime CreatedOnDate { get; set; } = DateTime.Now;
}
