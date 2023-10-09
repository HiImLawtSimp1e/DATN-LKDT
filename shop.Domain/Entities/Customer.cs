using shop.Domain.Entities.Base;
using System;

namespace shop.Domain.Entities;
public class Customer : BaseEntity
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Gender { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public bool EmailComfirmed { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public int Point { get; set; }
    public int Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public virtual ICollection<Order>? Orders { get; set; }
    public virtual Cart Cart { get; set; }
    public virtual ICollection<UsePointHistory>? UsePointHistories { get; set; }

}
