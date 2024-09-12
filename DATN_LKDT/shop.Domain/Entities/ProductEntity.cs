using shop.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class Product : BaseEntity
    {
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        [StringLength(250)]
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        [StringLength(70)]
        public string SeoTitle { get; set; } = string.Empty;
        [StringLength(160)]
        public string SeoDescription { get; set; } = string.Empty;
        [StringLength(100)]
        public string SeoKeyworks { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public Category? Category { get; set; }
        public Guid CategoryId { get; set; }
        public List<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
        public List<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
        public List<ProductValue> ProductValues { get; set; } = new List<ProductValue>();
    }
}
