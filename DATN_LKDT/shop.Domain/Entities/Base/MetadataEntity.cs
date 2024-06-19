using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities.Base;

[NotMapped]
public class MetadataEntity
{
    public Guid Id { get; set; }
    public virtual string FieldName { get; set; }
    public virtual string FieldValue { get; set; }
    public virtual string FieldValueTexts { get; set; }
}
