using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.API.Models
{
    public class Account
    {
        [Key]
        public Guid AccountId { get; set; }
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public string AccountIban { get; set; }
        [Required]
        [Column(TypeName = "money")]
        public decimal Balance { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
    }
}
