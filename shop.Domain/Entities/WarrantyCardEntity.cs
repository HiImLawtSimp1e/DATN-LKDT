using Newtonsoft.Json;
using shop.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;

namespace shop.Domain.Entities
{
    public class WarrantyCardEntity : BaseEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? IdBillDetail { get; set; }

        public Guid? IdWaranty { get; set; }
        public Guid? IdVirtualItem { get; set; }
        public string Type { get;set; }
        public string? Description { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public bool Isdelete { get; set; }  

        public int? Status { get; set; }

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

    }
}
