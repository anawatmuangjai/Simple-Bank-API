using SimpleBank.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bank.API.Infrastructure.Repositories.Interfaces
{
    public interface IWithdrawRepository
    {
        Task<List<Withdraw>> GetWithdrawByAccountIdAsync(Guid accountId);
        Task<Withdraw> AddWithdrawAsync(Withdraw withdraw);
    }
}
