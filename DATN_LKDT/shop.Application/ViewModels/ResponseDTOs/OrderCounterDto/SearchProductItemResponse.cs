﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.ResponseDTOs.OrderCounterDto
{
    public class SearchProductItemResponse
    {
        public Guid ProductId { get; set; }
        public Guid ProductTypeId { get; set; }
        public string ProductTitle { get; set; } = string.Empty;
        public string ProductTypeName { get; set; } = string.Empty;
        public int Price { get; set; }
        public int OriginalPrice { get; set; }
        public int Quantity { get; set; } = 1;
        public string ImageUrl { get; set; } = string.Empty;
    }
}
