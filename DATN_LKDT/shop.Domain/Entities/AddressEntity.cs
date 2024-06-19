namespace shop.Domain.Entities
{
    public class AddressEntity
    {
        public Guid Id { get; set; }

        public Guid IdAccount { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? District { get; set; }

        public string? HomeAddress { get; set; }

        public int Status { get; set; }

        public virtual AccountEntity Accounts { get; set; }
    }
}
