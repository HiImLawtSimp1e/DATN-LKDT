using shop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.ResponseDTOs.CustomerProductResponseDto
{
    public class CustomerProductResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string SeoTitle { get; set; } = string.Empty;
        public string SeoDescription { get; set; } = string.Empty;
        public string SeoKeyworks { get; set; } = string.Empty;
        public List<ProductVariantDto> ProductVariants { get; set; } = new List<ProductVariantDto>();
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public List<ProductValueDto>? ProductValues { get; set; } = new List<ProductValueDto>();
    }
}
