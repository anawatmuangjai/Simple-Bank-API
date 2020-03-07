using System;

namespace SimpleBank.API.Models.Dtos
{
    public class TransactionsResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string TransactionsType { get; set; }
        public DateTime TransactionsDate { get; set; }
    }
}
