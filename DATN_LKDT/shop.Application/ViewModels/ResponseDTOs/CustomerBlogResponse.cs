using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace shop.Application.ViewModels.ResponseDTOs
{
    public class CustomerBlogResponse
    {
        public string? Title { get; set; }
        public string IntroText { get; set; }

        [AllowHtml]
        public string? Content { get; set; }

        public string? Images { get; set; }
        public DateTime? CreatedOnDate { get; set; }
    }
}
