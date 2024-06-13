using AppData.Enum;
using shop.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class AccountEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string? Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }

        public UserTypeEnum UserTypeEnum { get; set; }
        public int Status { get; set; }

        public int? Points { get; set; }

        public virtual CartEntity? Carts { get; set; }
    }
}
