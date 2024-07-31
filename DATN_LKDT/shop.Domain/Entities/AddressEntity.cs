namespace shop.Domain.Entities
{
    public class AddressEntity
    {
        public Guid Id { get; set; }

        public Guid IdAccount { get; set; }


        public string City { get; set; } = string.Empty;

        public string District { get; set; } = string.Empty;

        public string HomeAddress { get; set; } = string.Empty;

        public bool IsActive = true;
        public bool Delete = false;

        public virtual AccountEntity Accounts { get; set; }
    }
}
