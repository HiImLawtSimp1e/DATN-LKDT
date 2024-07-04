using shop.Infrastructure.Model.Common.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Model
{
    public class ContactQueryModel: PaginationRequest
    {
        public string? CODE { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Content { get; set; }
        public int? Status { get; set; }
        public string? Type { get; set; }
        public string? Address { get; set; }
        public string? Topic { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
