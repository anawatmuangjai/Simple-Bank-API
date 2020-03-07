namespace SimpleBank.API.Models.Dtos
{
    public class AccountResponse
    {
        public string AccountIban { get; set; }
        public decimal Balance { get; set; }
    }
}
