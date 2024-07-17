using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class ProductImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public Guid ProductId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [JsonIgnore]
        public Product? Product { get; set; }
    }
}
