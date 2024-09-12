using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class ProductValue
    {
        [JsonIgnore]
        public Product? Product { get; set; }
        public Guid ProductId { get; set; }
        public ProductAttribute? ProductAttribute { get; set; }
        public Guid ProductAttributeId { get; set; }
        [StringLength(100)]
        public string Value { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool Deleted { get; set; } = false;
    }
}
