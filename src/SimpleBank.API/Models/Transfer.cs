using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.API.Models
{
    public class Transfer
    {
        [Key]
        public Guid TransferId { get; set; }
        [Required]
        public Guid FromAccountId { get; set; }
        [Required]
        public Guid ToAccountId { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
