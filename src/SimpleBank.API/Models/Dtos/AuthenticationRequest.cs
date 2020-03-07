using System.ComponentModel.DataAnnotations;

namespace SimpleBank.API.Models.Dtos
{
    public class AuthenticationRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
