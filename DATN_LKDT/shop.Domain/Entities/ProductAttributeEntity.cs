using shop.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class ProductAttribute : BaseEntity
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
        [JsonIgnore]
        public List<ProductValue>? ProductValues { get; set; }
    }
}
