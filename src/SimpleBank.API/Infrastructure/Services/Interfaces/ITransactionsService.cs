using SimpleBank.API.Models.Dtos;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Services.Interfaces
{
    public interface ITransactionsService
    {
        Task<TransactionsResult> DepositAsync(DepositRequest depositRequest);
        Task<TransactionsResult> WithdrawAsync(WithdrawRequest withdrawRequest);
        Task<TransactionsResult> TransferAsync(TransferRequest transferRequest);
    }
}
