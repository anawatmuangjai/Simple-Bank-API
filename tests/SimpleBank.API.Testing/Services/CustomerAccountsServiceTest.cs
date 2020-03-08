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
    public class CustomerAccountsServiceTest
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IAccountRepository accountRepository;
        private readonly CustomerAccountsService customerAccountsService;

        public CustomerAccountsServiceTest()
        {
            customerRepository = Substitute.For<ICustomerRepository>();
            accountRepository = Substitute.For<IAccountRepository>();

            customerAccountsService = new CustomerAccountsService(customerRepository, accountRepository);
        }

        [Fact]
        public async Task GetAccountAsync_WithAccountIbanExists_ReturnAccountResponse()
        {
            // Arrange
            var accountIban = "AAAAA";

            var account = new Account
            {
                AccountIban = "AAAAA",
                Balance = 1000
            };

            accountRepository
                .GetByAccountIbanAsync(Arg.Any<string>())
                .Returns(Task.FromResult(account));

            // Act
            var accountResponse = await customerAccountsService.GetAccountAsync(accountIban);

            // Assert
            Assert.NotNull(accountResponse);
            Assert.Equal(accountIban, accountResponse.AccountIban);
            Assert.Equal(1000, accountResponse.Balance);
        }

        [Fact]
        public async Task GetAccountAsync_WithAccountIbanNotExists_ReturnNull()
        {
            // Arrange
            var accountIban = "AAAAA";

            accountRepository
                .GetByAccountIbanAsync(Arg.Any<string>())
                .Returns(Task.FromResult((Account)null));

            // Act
            var accountResponse = await customerAccountsService.GetAccountAsync(accountIban);

            // Assert
            Assert.Null(accountResponse);
        }

        [Fact]
        public async Task CreateAccountAsync_NewAccountRequest_ReturnAccountResponse()
        {
            // Arrange 
            var accountRequest = new AccountRequest
            {
                AccountIban = "CCCC",
                FirstName = "test",
                LastName = "test",
                Balance = 1000,
                Email = "test@test.com",
                Address = "123456"
            };

            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = accountRequest.FirstName,
                LastName = accountRequest.LastName,
                Email = accountRequest.Email,
                Address = accountRequest.Address
            };

            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                CustomerId = customer.CustomerId,
                AccountIban = accountRequest.AccountIban,
                Balance = accountRequest.Balance
            };

            customerRepository
                .AddCustomerAsync(Arg.Any<Customer>())
                .Returns(Task.FromResult(customer));

            accountRepository
                .GetByAccountIbanAsync(Arg.Any<string>())
                .Returns(Task.FromResult(account));

            // Act
            var accountResponse = await customerAccountsService.CreateAccountAsync(accountRequest);

            // Assert
            Assert.NotNull(accountResponse);
            Assert.Equal("CCCC", accountResponse.AccountIban);
            Assert.Equal(1000, accountResponse.Balance);
        }

        [Fact]
        public async Task AccountExistsAsync_WithAccountAndCustomerNotExists_ReturnFalse()
        {
            // Arrange 
            var accountRequest = new AccountRequest
            {
                AccountIban = "CCCC",
                FirstName = "test",
                LastName = "test",
                Balance = 1000,
                Email = "test@test.com",
                Address = "123456"
            };

            customerRepository
               .CustomerExistsAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
               .Returns(false);

            accountRepository
                .AccountExistsAsync(Arg.Any<string>())
                .Returns(false);

            // Act
            var result = await customerAccountsService.AccountExistsAsync(accountRequest);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task AccountExistsAsync_WithAccountIbanExists_ReturnTrue()
        {
            // Arrange 
            var accountRequest = new AccountRequest
            {
                AccountIban = "CCCC",
                FirstName = "test",
                LastName = "test",
                Balance = 1000,
                Email = "test@test.com",
                Address = "123456"
            };

            customerRepository
               .CustomerExistsAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
               .Returns(false);

            accountRepository
                .AccountExistsAsync(Arg.Any<string>())
                .Returns(true);

            // Act
            var result = await customerAccountsService.AccountExistsAsync(accountRequest);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AccountExistsAsync_WithCustomerExists_ReturnTrue()
        {
            // Arrange 
            var accountRequest = new AccountRequest
            {
                AccountIban = "CCCC",
                FirstName = "test",
                LastName = "test",
                Balance = 1000,
                Email = "test@test.com",
                Address = "123456"
            };

            customerRepository
               .CustomerExistsAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())
               .Returns(true);

            accountRepository
                .AccountExistsAsync(Arg.Any<string>())
                .Returns(false);

            // Act
            var result = await customerAccountsService.AccountExistsAsync(accountRequest);

            // Assert
            Assert.True(result);
        }
    }
}
