using SimpleBank.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Repositories.Interfaces
{
    public interface IDepositRepository
    {
        Task<List<Deposit>> GetDepositByAccountIdAsync(Guid accountId);
        Task<Deposit> AddDepositAsync(Deposit deposit);
    }
}
