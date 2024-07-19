﻿using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class ProductVariant
    {
        [JsonIgnore]
        public Product? Product { get; set; }
        public Guid ProductId { get; set; }
        public ProductType? ProductType { get; set; }
        public Guid ProductTypeId { get; set; }
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        public int Quantity { get; set; } = 1000;
        public bool IsActive { get; set; } = true;
        public bool Deleted { get; set; } = false;
    }
}
