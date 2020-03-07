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
    public class CustomerAccountsService : ICustomerAccountsService
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IAccountRepository accountRepository;

        public CustomerAccountsService(ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            this.customerRepository = customerRepository;
            this.accountRepository = accountRepository;
        }

        public async Task<AccountResponse> GetAccountAsync(string accountIban)
        {
            var account = await accountRepository.GetByAccountIbanAsync(accountIban);

            if (account == null)
            {
                return null;
            }

            var accountResponse = new AccountResponse
            {
                AccountIban = account.AccountIban,
                Balance = account.Balance
            };

            return accountResponse;
        }

        public async Task<AccountResponse> CreateAccountAsync(AccountRequest request)
        {
            var customer = new Customer
            {
                CustomerId = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Address = request.Address
            };

            await customerRepository.AddCustomerAsync(customer);

            var account = new Account
            {
                AccountId = Guid.NewGuid(),
                CustomerId = customer.CustomerId,
                AccountIban = request.AccountIban,
                Balance = request.Balance
            };

            await accountRepository.AddAccountAsync(account);

            var accountResponse = new AccountResponse
            {
                AccountIban = account.AccountIban,
                Balance = account.Balance
            };

            return accountResponse;
        }

        public async Task<bool> AccountExistsAsync(AccountRequest request)
        {
            var customerExists = await customerRepository.CustomerExistsAsync(request.FirstName, request.LastName, request.Email);
            var accountExists = await accountRepository.AccountExistsAsync(request.AccountIban);

            if (customerExists || accountExists)
            {
                return true;
            }

            return false;
        }


    }
}
