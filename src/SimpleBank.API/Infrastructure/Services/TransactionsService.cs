using SimpleBank.API.Infrastructure.Repositories.Interfaces;
using SimpleBank.API.Infrastructure.Services.Interfaces;
using SimpleBank.API.Models;
using SimpleBank.API.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Services
{
    public class TransactionsService : ITransactionsService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IDepositRepository depositRepository;
        private readonly IWithdrawRepository withdrawRepository;
        private readonly ITransferRepository transferRepository;

        public TransactionsService(
            IAccountRepository accountRepository, 
            IDepositRepository depositRepository, 
            IWithdrawRepository withdrawRepository, 
            ITransferRepository transferRepository)
        {
            this.accountRepository = accountRepository;
            this.depositRepository = depositRepository;
            this.withdrawRepository = withdrawRepository;
            this.transferRepository = transferRepository;
        }

        public async Task<TransactionsResult> DepositAsync(DepositRequest depositRequest)
        {
            var transactionsResult = new TransactionsResult
            {
                AccountIban = depositRequest.AccountIban,
                Amount = depositRequest.Amount,
                Success = false,
                ErrorMessage = string.Empty,
                TransactionsType = "Deposit",
                TransactionsDate = DateTime.UtcNow
            };

            if (depositRequest.Amount <= 0)
            {
                transactionsResult.ErrorMessage = "Deposit amount is incorret";
                return transactionsResult;
            }

            var account = await accountRepository.GetByAccountIbanAsync(depositRequest.AccountIban);

            if (account == null)
            {
                transactionsResult.ErrorMessage = "Account does not exists";
                return transactionsResult;
            }

            var amount = (double)depositRequest.Amount;
            var fee = amount * 0.1 / 100;
            var total = amount - fee;
            account.Balance += (decimal)total;

            await accountRepository.UpdateAccountAsync(account);

            var deposit = new Deposit
            {
                DepositId = Guid.NewGuid(),
                AccountId = account.AccountId,
                Amount = depositRequest.Amount,
                Timestamp = DateTime.UtcNow
            };

            await depositRepository.AddDepositAsync(deposit);

            transactionsResult.Balance = account.Balance;
            transactionsResult.Success = true;

            return transactionsResult;
        }

        public async Task<TransactionsResult> WithdrawAsync(WithdrawRequest withdrawRequest)
        {
            var transactionsResult = new TransactionsResult
            {
                AccountIban = withdrawRequest.AccountIban,
                Amount = withdrawRequest.Amount,
                Success = false,
                ErrorMessage = string.Empty,
                TransactionsType = "Withdraw",
                TransactionsDate = DateTime.UtcNow
            };

            if (withdrawRequest.Amount <= 0)
            {
                transactionsResult.ErrorMessage = "Withdraw amount is incorret";
                return transactionsResult;
            }

            var account = await accountRepository.GetByAccountIbanAsync(withdrawRequest.AccountIban);

            if (account == null)
            {
                transactionsResult.ErrorMessage = "Account does not exists";
                return transactionsResult;
            }

            if (account.Balance - withdrawRequest.Amount < 0)
            {
                transactionsResult.ErrorMessage = "Account balance less than withdraw amount";
                return transactionsResult;
            }

            account.Balance -= withdrawRequest.Amount;

            await accountRepository.UpdateAccountAsync(account);

            var withdraw = new Withdraw
            {
                WithdrawId = Guid.NewGuid(),
                AccountId = account.AccountId,
                Amount = withdrawRequest.Amount,
                Timestamp = DateTime.UtcNow
            };

            await withdrawRepository.AddWithdrawAsync(withdraw);

            transactionsResult.Balance = account.Balance;
            transactionsResult.Success = true;

            return transactionsResult;
        }

        public async Task<TransactionsResult> TransferAsync(TransferRequest transferRequest)
        {
            var transactionsResult = new TransactionsResult
            {
                AccountIban = transferRequest.FromAccountIban,
                Amount = transferRequest.Amount,
                Success = false,
                ErrorMessage = string.Empty,
                TransactionsType = "Transfer",
                TransactionsDate = DateTime.UtcNow
            };

            var accountFrom = await accountRepository.GetByAccountIbanAsync(transferRequest.FromAccountIban);
            var accountTo = await accountRepository.GetByAccountIbanAsync(transferRequest.ToAccountIban);

            if (accountFrom == null || accountTo == null)
            {
                transactionsResult.ErrorMessage = "Account does not exists";
                return transactionsResult;
            }

            if (accountFrom.Balance - transferRequest.Amount < 0)
            {
                transactionsResult.ErrorMessage = "Account balance not enough";
                return transactionsResult;
            }

            accountFrom.Balance -= transferRequest.Amount;
            accountTo.Balance += transferRequest.Amount;

            await accountRepository.UpdateAccountAsync(accountFrom);
            await accountRepository.UpdateAccountAsync(accountTo);

            var transfer = new Transfer
            {
                TransferId = Guid.NewGuid(),
                FromAccountId = accountFrom.AccountId,
                ToAccountId = accountTo.AccountId,
                Amount = transferRequest.Amount,
                Timestamp = DateTime.UtcNow
            };

            await transferRepository.AddTransferAsync(transfer);

            transactionsResult.Balance = accountFrom.Balance;
            transactionsResult.Success = true;

            return transactionsResult;
        }

    }
}
