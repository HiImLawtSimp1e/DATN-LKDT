using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}
