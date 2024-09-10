using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities.Base;

[NotMapped]
public class BaseEntity
{
    [StringLength(100)]
    public virtual string CreatedBy { get; set; } = "admin@fshop.com";
    [StringLength(100)]
    public virtual string ModifiedBy { get; set; } = string.Empty;

    public virtual DateTime ModifiedAt { get; set; } = DateTime.Now;

    public virtual DateTime CreatedAt { get; set; } = DateTime.Now;
}
