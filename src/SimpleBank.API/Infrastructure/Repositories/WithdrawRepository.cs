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
    public class WithdrawRepository : IWithdrawRepository
    {
        private readonly ApplicationDbContext dbContext;

        public WithdrawRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Withdraw> AddWithdrawAsync(Withdraw withdraw)
        {
            await dbContext.Withdraws.AddAsync(withdraw);
            await dbContext.SaveChangesAsync();
            return withdraw;
        }

        public async Task<List<Withdraw>> GetWithdrawByAccountIdAsync(Guid accountId)
        {
            return await dbContext.Withdraws.Where(x => x.AccountId == accountId).ToListAsync();
        }
    }
}
