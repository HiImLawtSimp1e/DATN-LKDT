using AppData.Enum;
using shop.Domain.Entities.Base;
using System.Text.Json.Serialization;

namespace shop.Domain.Entities
{
    public class AccountEntity : BaseEntity
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string? Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public UserTypeEnum Role { get; set; } = UserTypeEnum.Customer;
        public int Status { get; set; }

        public int? Points { get; set; }

        public virtual CartEntity? Carts { get; set; }
        [JsonIgnore]
        public List<Order>? Orders { get; set; }
    }
}
