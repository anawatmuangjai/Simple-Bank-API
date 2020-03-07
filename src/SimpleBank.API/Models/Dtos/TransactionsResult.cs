using System;

namespace SimpleBank.API.Models.Dtos
{
    public class TransactionsResult
    {
        public string AccountIban { get; set; }
        public string TransactionsType { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime TransactionsDate { get; set; }
    }
}
