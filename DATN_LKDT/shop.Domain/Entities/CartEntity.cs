using System.ComponentModel.DataAnnotations;
using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class CartEntity : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid? IdAccount { get; set; }
        public string UserName { get; set; }
        public virtual AccountEntity? Accounts { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
