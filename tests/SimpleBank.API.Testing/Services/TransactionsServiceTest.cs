using SimpleBank.API.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;
using SimpleBank.API.Infrastructure.Services;
using System.Threading.Tasks;
using SimpleBank.API.Models.Dtos;
using SimpleBank.API.Models;

namespace SimpleBank.API.Testing.Services
{
    public class TransactionsServiceTest
    {
        private readonly IAccountRepository accountRepository;
        private readonly IDepositRepository depositRepository;
        private readonly IWithdrawRepository withdrawRepository;
        private readonly ITransferRepository transferRepository;
        private readonly TransactionsService transactionsService;

        public TransactionsServiceTest()
        {
            accountRepository = Substitute.For<IAccountRepository>();
            depositRepository = Substitute.For<IDepositRepository>();
            withdrawRepository = Substitute.For<IWithdrawRepository>();
            transferRepository = Substitute.For<ITransferRepository>();

            transactionsService = new TransactionsService(accountRepository, depositRepository, withdrawRepository, transferRepository);
        }

        [Fact]
        public async Task DepositAsync_WithAmountMoreThanZero_ReturnTransactionsResult()
        {
            // Arrange
            var depositRequest = new DepositRequest
            {
                AccountIban = "TH123456",
                Amount = 1000,
            };

            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountIban = "TH123456",
                Balance = 1000,
            };

            accountRepository.GetByAccountIbanAsync(Arg.Any<string>()).Returns(Task.FromResult(account));
            accountRepository.UpdateAccountAsync(Arg.Any<Account>()).Returns(Task.FromResult(new Account()));
            depositRepository.AddDepositAsync(Arg.Any<Deposit>()).Returns(Task.FromResult(new Deposit()));

            // Act
            var transactionsResult = await transactionsService.DepositAsync(depositRequest);

            // Assert
            Assert.True(transactionsResult.Success);
            Assert.Equal(1999, transactionsResult.Balance);
        }

        [Fact]
        public async Task DepositAsync_WithAmountZero_ReturnTransactionsResult()
        {
            // Arrange
            var depositRequest = new DepositRequest
            {
                AccountIban = "TH123456",
                Amount = 0,
            };

            // Act
            var transactionsResult = await transactionsService.DepositAsync(depositRequest);

            // Assert
            Assert.False(transactionsResult.Success);
            Assert.Equal("Deposit amount is incorret", transactionsResult.ErrorMessage);
        }

        [Fact]
        public async Task DepositAsync_WithIncorrectAccount_ReturnTransactionsResult()
        {
            // Arrange
            var depositRequest = new DepositRequest
            {
                AccountIban = "TH999999",
                Amount = 1000,
            };

            accountRepository.GetByAccountIbanAsync(Arg.Any<string>()).Returns(Task.FromResult((Account)null));

            // Act
            var transactionsResult = await transactionsService.DepositAsync(depositRequest);

            // Assert
            Assert.False(transactionsResult.Success);
            Assert.Equal("Account does not exists", transactionsResult.ErrorMessage);
        }

        [Fact]
        public async Task WithdrawAsync_WithAmountMoreThanZero_ReturnTransactionsResult()
        {
            // Arrange
            var withdrawRequest = new WithdrawRequest
            {
                AccountIban = "TH123456",
                Amount = 200,
            };

            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountIban = "TH123456",
                Balance = 1000,
            };

            accountRepository.GetByAccountIbanAsync(Arg.Any<string>()).Returns(Task.FromResult(account));
            accountRepository.UpdateAccountAsync(Arg.Any<Account>()).Returns(Task.FromResult(new Account()));
            withdrawRepository.AddWithdrawAsync(Arg.Any<Withdraw>()).Returns(Task.FromResult(new Withdraw()));

            // Act
            var transactionsResult = await transactionsService.WithdrawAsync(withdrawRequest);

            // Assert
            Assert.True(transactionsResult.Success);
            Assert.Equal(800, transactionsResult.Balance);
        }

        [Fact]
        public async Task WithdrawAsync_WithAmountZero_ReturnTransactionsResult()
        {
            // Arrange
            var withdrawRequest = new WithdrawRequest
            {
                AccountIban = "TH123456",
                Amount = 0,
            };

            // Act
            var transactionsResult = await transactionsService.WithdrawAsync(withdrawRequest);

            // Assert
            Assert.False(transactionsResult.Success);
            Assert.Equal("Withdraw amount is incorret", transactionsResult.ErrorMessage);
        }

        [Fact]
        public async Task WithdrawAsync_WithIncorrectAccount_ReturnTransactionsResult()
        {
            // Arrange
            var withdrawRequest = new WithdrawRequest
            {
                AccountIban = "TH123456",
                Amount = 1000,
            };

            accountRepository.GetByAccountIbanAsync(Arg.Any<string>()).Returns(Task.FromResult((Account)null));

            // Act
            var transactionsResult = await transactionsService.WithdrawAsync(withdrawRequest);

            // Assert
            Assert.False(transactionsResult.Success);
            Assert.Equal("Account does not exists", transactionsResult.ErrorMessage);
        }

        [Fact]
        public async Task WithdrawAsync_WithBalanceLessThanAmount_ReturnTransactionsResult()
        {
            // Arrange
            var withdrawRequest = new WithdrawRequest
            {
                AccountIban = "TH123456",
                Amount = 2000,
            };

            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountIban = "TH123456",
                Balance = 1000,
            };

            accountRepository.GetByAccountIbanAsync(Arg.Any<string>()).Returns(Task.FromResult(account));

            // Act
            var transactionsResult = await transactionsService.WithdrawAsync(withdrawRequest);

            // Assert
            Assert.False(transactionsResult.Success);
            Assert.Equal("Account balance less than withdraw amount", transactionsResult.ErrorMessage);
        }

        [Fact]
        public async Task TransferAsync_WithCorrectAmount_ReturnTransactionsResult()
        {
            var transferRequest = new TransferRequest
            {
                FromAccountIban = "AAAA",
                ToAccountIban = "BBBB",
                Amount = 1000
            };

            var fromAccount = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountIban = "AAAA",
                Balance = 2000,
            };

            var toAccount = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountIban = "BBBB",
                Balance = 500,
            };

            accountRepository.GetByAccountIbanAsync(transferRequest.FromAccountIban).Returns(Task.FromResult(fromAccount));
            accountRepository.GetByAccountIbanAsync(transferRequest.ToAccountIban).Returns(Task.FromResult(toAccount));
            accountRepository.UpdateAccountAsync(fromAccount).Returns(Task.FromResult(new Account()));
            accountRepository.UpdateAccountAsync(toAccount).Returns(Task.FromResult(new Account()));
            transferRepository.AddTransferAsync(Arg.Any<Transfer>()).Returns(Task.FromResult(new Transfer()));

            // Act
            var transactionsResult = await transactionsService.TransferAsync(transferRequest);

            // Assert
            Assert.True(transactionsResult.Success);
            Assert.Equal(1000, transactionsResult.Balance);
        }

        [Fact]
        public async Task TransferAsync_WithIncorrectAmount_ReturnTransactionsResult()
        {
            var transferRequest = new TransferRequest
            {
                FromAccountIban = "AAAA",
                ToAccountIban = "BBBB",
                Amount = 1000
            };

            var fromAccount = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountIban = "AAAA",
                Balance = 500,
            };

            var toAccount = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountIban = "BBBB",
                Balance = 500,
            };

            accountRepository.GetByAccountIbanAsync(transferRequest.FromAccountIban).Returns(Task.FromResult(fromAccount));
            accountRepository.GetByAccountIbanAsync(transferRequest.ToAccountIban).Returns(Task.FromResult(toAccount));
            accountRepository.UpdateAccountAsync(fromAccount).Returns(Task.FromResult(new Account()));
            accountRepository.UpdateAccountAsync(toAccount).Returns(Task.FromResult(new Account()));
            transferRepository.AddTransferAsync(Arg.Any<Transfer>()).Returns(Task.FromResult(new Transfer()));

            // Act
            var transactionsResult = await transactionsService.TransferAsync(transferRequest);

            // Assert
            Assert.False(transactionsResult.Success);
            Assert.Equal("Account balance not enough", transactionsResult.ErrorMessage);
        }

        [Fact]
        public async Task TransferAsync_WithIncorrectAccount_ReturnTransactionsResult()
        {
            var transferRequest = new TransferRequest
            {
                FromAccountIban = "AAAA",
                ToAccountIban = "CCCC",
                Amount = 1000
            };

            var fromAccount = new Account
            {
                AccountId = Guid.NewGuid(),
                AccountIban = "AAAA",
                Balance = 2000,
            };

            accountRepository.GetByAccountIbanAsync(transferRequest.FromAccountIban).Returns(Task.FromResult(fromAccount));
            accountRepository.GetByAccountIbanAsync(transferRequest.ToAccountIban).Returns(Task.FromResult((Account)null));

            // Act
            var transactionsResult = await transactionsService.TransferAsync(transferRequest);

            // Assert
            Assert.False(transactionsResult.Success);
            Assert.Equal("Account does not exists", transactionsResult.ErrorMessage);
        }
    }
}
