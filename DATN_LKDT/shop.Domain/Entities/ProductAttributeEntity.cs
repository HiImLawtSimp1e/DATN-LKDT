using shop.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class ProductAttribute : BaseEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Deleted { get; set; } = false;
        [JsonIgnore]
        public List<ProductValue>? ProductValues { get; set; }
    }
}
