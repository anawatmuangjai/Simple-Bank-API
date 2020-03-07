using System.ComponentModel.DataAnnotations;

namespace SimpleBank.API.Models.Dtos
{
    public class DepositRequest
    {
        [Required]
        public string AccountIban { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
