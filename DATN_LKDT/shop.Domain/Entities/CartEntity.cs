using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class CartEntity
    {
        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public Guid? AccountId { get; set; }
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
