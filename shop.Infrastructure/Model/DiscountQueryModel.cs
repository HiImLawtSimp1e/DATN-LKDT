using shop.Infrastructure.Model.Common.Pagination;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Infrastructure.Model
{
    public class DiscountQueryModel: PaginationRequest
    {
        public string? MaVoucher { get; set; }

        public string? NameVoucher { get; set; }

        public int? DieuKien { get; set; }
        public int? TypeVoucher { get; set; }

        public int? Quantity { get; set; }

        public double? MucUuDai { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }
        public string? StatusVoucher { get; set; }
    }
}
