using Newtonsoft.Json;
using shop.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities
{
    public class RefundEntity : BaseEntity
    {
        public Guid Id { get; set; }

        public string? SerrialId { get; set; }

        public string? ItemName { get; set; }

        public decimal? Price { get; set; }

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
        public int? StatusDetail { get; set; }


    }
}
