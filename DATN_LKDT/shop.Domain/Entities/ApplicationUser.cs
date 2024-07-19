using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace shop.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? CitizenId { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }
        public int Status { get; set; }
        [NotMapped]
        public IFormFile Images { get; set; }
        [NotMapped]
        public string? Role { get; set; }

    }
}
