using SimpleBank.API.Infrastructure.Data;
using SimpleBank.API.Infrastructure.Repositories.Interfaces;
using SimpleBank.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext dbContext;

        public AccountRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Account> GetByAccountIbanAsync(string accountIban)
        {
            return await dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountIban == accountIban);
        }

        public async Task<Account> AddAccountAsync(Account account)
        {
            await dbContext.Accounts.AddAsync(account);
            await dbContext.SaveChangesAsync();
            return account;
        }

        public async Task<Account> UpdateAccountAsync(Account updateAccount)
        {
            var account = await dbContext.Accounts.FirstOrDefaultAsync(x => x.AccountId == updateAccount.AccountId);

            account.Balance = updateAccount.Balance;

            await dbContext.SaveChangesAsync();

            return account;
        }

        public async Task<bool> AccountExistsAsync(string accountIban)
        {
            return await dbContext.Accounts.AnyAsync(x => x.AccountIban == accountIban);
        }


    }
}
