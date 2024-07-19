using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Domain.EntitiesInterface
{
    public interface IIdEntity
    {
        Guid Id { get; set; }
    }
}
