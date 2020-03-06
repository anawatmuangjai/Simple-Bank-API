using SimpleBank.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Repositories.Interfaces
{
    public interface ITransferRepository
    {
        Task<List<Transfer>> GetTransferByAccountIdAsync(Guid accountIdFrom);
        Task<Transfer> AddTransferAsync(Transfer transfer);
    }
}
