using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class ShopInformationeEntity
    {
        public string? Name { get; set; }
        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Email { get; set; }

        public string? Fax { get; set; }

        public string? Note { get; set; }

        public int? Status { get; set; }
    }
}
