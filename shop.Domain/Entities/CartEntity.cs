using System.ComponentModel.DataAnnotations;
using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class CartEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid? IdAccount { get; set; }
        public string UserName { get; set; }
        public string? Description { get; set; }
        public virtual AccountEntity? Accounts { get; set; }
        public virtual List<CartDetailsEntity>? CartDetails { get; set; }
    }
}
