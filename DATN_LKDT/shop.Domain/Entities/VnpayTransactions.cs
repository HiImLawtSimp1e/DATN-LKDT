using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop.Domain.Entities
{
    public class VnpayTransactions
    {
        [Required]
        [StringLength(30)]
        public string TransactionId { get; set; } = string.Empty;   
        public Guid UserId { get; set; }
        public Guid? VoucherId { get; set; }
        [StringLength(30)]
        public string Status { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
