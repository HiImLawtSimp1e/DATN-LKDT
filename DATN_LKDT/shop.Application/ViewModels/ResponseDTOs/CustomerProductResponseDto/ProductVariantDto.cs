using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.ResponseDTOs.CustomerProductResponseDto
{
    public class ProductVariantDto
    {
        public Guid ProductId { get; set; }
        public ProductTypeDto? ProductType { get; set; }
        public Guid ProductTypeId { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public int Quantity { get; set; }

    }
}
