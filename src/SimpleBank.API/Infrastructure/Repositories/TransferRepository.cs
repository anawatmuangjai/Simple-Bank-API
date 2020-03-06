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
    public class TransferRepository : ITransferRepository
    {
        private readonly ApplicationDbContext dbContext;

        public TransferRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Transfer> AddTransferAsync(Transfer transfer)
        {
            await dbContext.Transfers.AddAsync(transfer);
            await dbContext.SaveChangesAsync();
            return transfer;
        }

        public async Task<List<Transfer>> GetTransferByAccountIdAsync(Guid accountIdFrom)
        {
            return await dbContext.Transfers.Where(x => x.FromAccountId == accountIdFrom).ToListAsync();
        }
    }
}
