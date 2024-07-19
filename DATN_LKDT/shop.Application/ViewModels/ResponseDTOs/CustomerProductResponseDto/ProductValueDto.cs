using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.ResponseDTOs.CustomerProductResponseDto
{
    public class ProductValueDto
    {
        public Guid ProductId { get; set; }
        public ProductAttributeDto? ProductAttribute { get; set; }
        public Guid ProductAttributeId { get; set; }
        public string Value { get; set; } = string.Empty;
    }
}
