using Newtonsoft.Json;
using shop.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities;

public class VirtualItemObjRelationEntity :BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public Guid? VirtualItemId { get; set; }
    public Guid? ObjectId { get; set; }
    public int? Order { get; set; }
    [NotMapped]
    public virtual RelatedObjEntity RelatedObj { get; set; }
    public virtual string RelatedObjJson
    {
        get
        {
            return RelatedObj == null ? null : JsonConvert.SerializeObject(RelatedObj);
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                RelatedObj = null;
            else
            {
                try
                {
                    RelatedObj = JsonConvert.DeserializeObject<RelatedObjEntity>(value);
                }
                catch
                {
                    RelatedObj = null;
                }
            }
        }
    }
    [NotMapped]
    public virtual RelatedObjEntity RelatedObj2 { get; set; }
    public virtual string RelatedObj2Json
    {
        get
        {
            return RelatedObj2 == null ? null : JsonConvert.SerializeObject(RelatedObj2);
        }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                RelatedObj2 = null;
            else
            {
                try
                {
                    RelatedObj2 = JsonConvert.DeserializeObject<RelatedObjEntity>(value);
                }
                catch
                {
                    RelatedObj2 = null;
                }
            }
        }
    }
    public string? RelationType { get;set; }
    public bool? Isdeleted { get; set; } =false;
}
