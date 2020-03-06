using SimpleBank.API.Models;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account> GetByAccountIbanAsync(string accountIban);
        Task<Account> AddAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(Account account);
        Task<bool> AccountExistsAsync(string accountIban);
    }
}
