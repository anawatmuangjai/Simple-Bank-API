using System.ComponentModel.DataAnnotations;

namespace SimpleBank.API.Models.Dtos
{
    public class AccountRequest
    {
        [Required]
        public string AccountIban { get; set; }
        [Required]
        public decimal Balance { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
