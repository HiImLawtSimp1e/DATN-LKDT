﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace shop.Application.ViewModels.ResponseDTOs
{
    public class CustomerBlogResponse
    {
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [AllowHtml]
        public string Content { get; set; } = string.Empty;
        public string SeoTitle { get; set; } = string.Empty;
        public string SeoDescription { get; set; } = string.Empty;
        public string SeoKeyworks { get; set; } = string.Empty;
        public DateTime CreatedOnDate { get; set; }
    }
}
