using System.ComponentModel.DataAnnotations;

namespace SimpleBank.API.Models.Dtos
{
    public class TransferRequest
    {
        [Required]
        public string FromAccountIban { get; set; }
        [Required]
        public string ToAccountIban { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
