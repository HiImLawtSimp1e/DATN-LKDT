using AppData.Enum;
using shop.Domain.Entities.Base;
using System.Data;
using System.Text.Json.Serialization;

namespace shop.Domain.Entities
{
    public class AccountEntity : BaseEntity
    {
        public Guid Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
        public int? Points { get; set; }
        public bool IsActive { get; set; } = true;
        public bool Deleted { get; set; } = false;
        public Guid RoleId { get; set; }
        public RoleEntity? Role { get; set; }

        [JsonIgnore]

        public CartEntity? Cart { get; set; }
        [JsonIgnore]
        public List<Order>? Orders { get; set; }
    }
}
