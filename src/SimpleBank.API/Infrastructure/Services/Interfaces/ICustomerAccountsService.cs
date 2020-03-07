using SimpleBank.API.Models.Dtos;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Services.Interfaces
{
    public interface ICustomerAccountsService 
    {
        Task<AccountResponse> GetAccountAsync(string accountIban);
        Task<AccountResponse> CreateAccountAsync(AccountRequest account);
        Task<bool> AccountExistsAsync(AccountRequest account);
    }
}
