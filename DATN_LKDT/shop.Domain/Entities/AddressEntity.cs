using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace shop.Domain.Entities
{
    public class AddressEntity
    {
        public Guid Id { get; set; }
        [StringLength(50, MinimumLength = 6)]
        public string Name { get; set; } = string.Empty;
        [StringLength(30, MinimumLength = 6)]
        public string Email { get; set; } = string.Empty;
        [StringLength(14)]
        public string PhoneNumber { get; set; } = string.Empty;
        [StringLength(250, MinimumLength = 6)]
        public string Address { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public Guid AccountId { get; set; }
        [JsonIgnore]
        public virtual AccountEntity? Account { get; set; }
    }
}
