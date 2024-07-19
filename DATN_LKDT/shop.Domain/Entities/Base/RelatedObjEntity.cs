using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities.Base
{
    [NotMapped]
    public class RelatedObjEntity
    {
        public string ObjectId { get; set; }

        public string ObjectCode { get; set; }

        public string ObjectName { get; set; }

        public string ObjectType { get; set; }

        public virtual string ObjectContent { get; set; }
    }
}
