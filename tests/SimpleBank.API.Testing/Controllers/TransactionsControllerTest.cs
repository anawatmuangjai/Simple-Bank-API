using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SimpleBank.API.Controllers;
using SimpleBank.API.Infrastructure.Services.Interfaces;
using SimpleBank.API.Models.Dtos;
using Xunit;

namespace SimpleBank.API.Testing.Controllers
{
    public class TransactionsControllerTest
    {
        private readonly ITransactionsService transactionsService;
        private readonly TransactionsController transactionsController;

        public TransactionsControllerTest()
        {
            transactionsService = Substitute.For<ITransactionsService>();

            transactionsController = new TransactionsController(transactionsService);
        }

        [Fact]
        public async Task Deposit_WithCorrectRequest_ReturnSuccessTransactionsResult()
        {
            // Arrange
            var depositRequest = new DepositRequest
            {
                AccountIban = "TH123456",
                Amount = 1000,
            };

            var transactionsResult = new TransactionsResult
            {
                Success = true
            };

            transactionsService
                .DepositAsync(Arg.Any<DepositRequest>())
                .Returns(Task.FromResult(transactionsResult));

            // Act
            var actionResult = await transactionsController.Deposit(depositRequest);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
            Assert.True((objectResult.Value as TransactionsResult).Success);
        }

        [Fact]
        public async Task Deposit_WithIncorrectRequest_ReturnFailedTransactionsResult()
        {
            // Arrange
            var depositRequest = new DepositRequest
            {
                AccountIban = "TH123456",
                Amount = 0,
            };

            var transactionsResult = new TransactionsResult
            {
                Success = false
            };

            transactionsService
                .DepositAsync(Arg.Any<DepositRequest>())
                .Returns(Task.FromResult(transactionsResult));

            // Act
            var actionResult = await transactionsController.Deposit(depositRequest);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
            Assert.False((objectResult.Value as TransactionsResult).Success);

        }

        [Fact]
        public async Task Withdraw_WithCorrectRequest_ReturnSuccessTransactionsResult()
        {
            // Arrange
            var withdrawRequest = new WithdrawRequest
            {
                AccountIban = "TH123456",
                Amount = 500,
            };

            var transactionsResult = new TransactionsResult
            {
                Success = true
            };

            transactionsService
                .WithdrawAsync(Arg.Any<WithdrawRequest>())
                .Returns(Task.FromResult(transactionsResult));

            // Act
            var actionResult = await transactionsController.Withdraw(withdrawRequest);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
            Assert.True((objectResult.Value as TransactionsResult).Success);
        }

        [Fact]
        public async Task Withdraw_WithIncorrectRequest_ReturnFailedTransactionsResult()
        {
            // Arrange
            var withdrawRequest = new WithdrawRequest
            {
                AccountIban = "TH123456",
                Amount = 500,
            };

            var transactionsResult = new TransactionsResult
            {
                Success = false
            };

            transactionsService
                .WithdrawAsync(Arg.Any<WithdrawRequest>())
                .Returns(Task.FromResult(transactionsResult));

            // Act
            var actionResult = await transactionsController.Withdraw(withdrawRequest);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
            Assert.False((objectResult.Value as TransactionsResult).Success);
        }
    
        [Fact]
        public async Task Transfer_WithCorrectRequest_ReturnSuccessTransactionsResult()
        {
            // Arrange
            var transferRequest = new TransferRequest
            {
                FromAccountIban = "AAAAA",
                ToAccountIban = "BBBBB",
                Amount = 1000,
            };

            var transactionsResult = new TransactionsResult
            {
                Success = true
            };

            transactionsService
                .TransferAsync(Arg.Any<TransferRequest>())
                .Returns(Task.FromResult(transactionsResult));

            // Act
            var actionResult = await transactionsController.Transfer(transferRequest);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
            Assert.True((objectResult.Value as TransactionsResult).Success);
        }

        [Fact]
        public async Task Transfer_WithIncorrectRequest_ReturnSuccessTransactionsResult()
        {
            // Arrange
            var transferRequest = new TransferRequest
            {
                FromAccountIban = "AAAAA",
                ToAccountIban = "BBBBB",
                Amount = 1000,
            };

            var transactionsResult = new TransactionsResult
            {
                Success = false
            };

            transactionsService
                .TransferAsync(Arg.Any<TransferRequest>())
                .Returns(Task.FromResult(transactionsResult));

            // Act
            var actionResult = await transactionsController.Transfer(transferRequest);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, objectResult.StatusCode);
            Assert.False((objectResult.Value as TransactionsResult).Success);
        }
    }
}
