using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Application.ViewModels.ResponseDTOs
{
    public class CategorySelectResponseDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
