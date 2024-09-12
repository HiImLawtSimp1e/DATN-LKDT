using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class RoleEntity
    {
        public Guid Id { get; set; }
        [StringLength(50)]
        public string RoleName { get; set; } = string.Empty;
    }
}
