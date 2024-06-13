using System.ComponentModel.DataAnnotations;

namespace shop.Domain.Entities
{
    public class ProductionCompanyEntity
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên.")]
        [StringLength(50, ErrorMessage = "Tên không được vượt quá 50 ký tự.")]
        public string? Name { get; set; }
    }
}
