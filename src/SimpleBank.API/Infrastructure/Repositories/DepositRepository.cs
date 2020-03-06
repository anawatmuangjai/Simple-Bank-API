using SimpleBank.API.Infrastructure.Data;
using SimpleBank.API.Infrastructure.Repositories.Interfaces;
using SimpleBank.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Repositories
{
    public class DepositRepository : IDepositRepository
    {
        private readonly ApplicationDbContext dbContext;

        public DepositRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Deposit> AddDepositAsync(Deposit deposit)
        {
            await dbContext.Deposits.AddAsync(deposit);
            await dbContext.SaveChangesAsync();
            return deposit;
        }

        public async Task<List<Deposit>> GetDepositByAccountIdAsync(Guid accountId)
        {
            return await dbContext.Deposits.Where(x => x.AccountId == accountId).ToListAsync();
        }
    }
}
