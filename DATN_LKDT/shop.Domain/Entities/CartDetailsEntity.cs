using Newtonsoft.Json;
using shop.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities
{
    public class CartDetailsEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }
        public Guid? IdUser { get; set; }
        public string? IdVirtualItem { get; set; }
        public int? Status { get; set; }
        public string? MetadataJson
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
        public string? Decription { get; set; }
        [NotMapped]
        public List<MetadataEntity>? Metadata { get; set; }
        public virtual CartEntity Carts { get; set; }
    }
}
