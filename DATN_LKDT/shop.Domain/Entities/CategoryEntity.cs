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
    public class Category : BaseEntity
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [JsonIgnore]
        public List<Product> Products { get; set; }
    }
}
