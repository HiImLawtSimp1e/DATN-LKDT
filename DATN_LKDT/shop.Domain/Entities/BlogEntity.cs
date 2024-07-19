﻿using shop.Domain.Entities.Base;
using System.Web.Mvc;

namespace shop.Domain.Entities
{
    public class BlogEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [AllowHtml]
        public string Content { get; set; } = string.Empty;
        public string SeoTitle { get; set; } = string.Empty;
        public string SeoDescription { get; set; } = string.Empty;
        public string SeoKeyworks { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool Deleted { get; set; } = false;
    }
}
