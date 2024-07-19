using Newtonsoft.Json;
using shop.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities
{
    [Table("VirtualItem")]
    public class VirtualItemEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid Code { get; set; }
        public string ParenId { get; set; }
        public string Type { get; set; }
        public string? Name { get; set; }
        public string VirtualType { get; set; }
        public bool? Isdeleted { get; set; }
        public bool? Ispublish { get; set; }
        public string? ImgUrl { get; set; }
        public string? Decription { get; set; }

        public string MetadataJson
        {
            get
            {
                if (Metadata != null)
                {
                    return JsonConvert.SerializeObject(Metadata);
                }
                return null;
            }
            set
            {
                try
                {
                    Metadata = JsonConvert.DeserializeObject<List<MetadataEntity>>(value);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }
        [NotMapped]
        public List<MetadataEntity> Metadata { get; set; }
        public int? Solid { get; set; }
        public int Status { get; set; }
    }
}
