using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using shop.Domain.Entities.Base;

namespace shop.Domain.Entities
{
    public class CartEntity
    {
        public Guid Id { get; set; }
        public Guid? IdAccount { get; set; }
        public string UserName { get; set; } = string.Empty;
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
