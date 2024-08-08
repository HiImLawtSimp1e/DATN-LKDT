using System.Text.Json.Serialization;

namespace shop.Domain.Entities
{
    public class AddressEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public bool IsMain { get; set; } = true;
        public Guid AccountId { get; set; }
        [JsonIgnore]
        public virtual AccountEntity? Account { get; set; }
    }
}
