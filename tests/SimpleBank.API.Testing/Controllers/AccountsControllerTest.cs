using SimpleBank.API.Controllers;
using SimpleBank.API.Infrastructure.Services.Interfaces;
using NSubstitute;
using Xunit;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SimpleBank.API.Models.Dtos;

namespace SimpleBank.API.Testing.Controllers
{
    public class AccountsControllerTest
    {
        private readonly ICustomerAccountsService customerAccountsService;
        private readonly AccountsController accountsController;

        public AccountsControllerTest()
        {
            customerAccountsService = Substitute.For<ICustomerAccountsService>();

            accountsController = new AccountsController(customerAccountsService);
        }

        [Fact]
        public async Task GetAccountByIban_WithAccountIbanExsits_ReturnStatusCodeOK()
        {
            // Arrange
            var accountIban = "AAAA";

            var accountResponse = new AccountResponse
            {
                AccountIban = "AAAA",
                Balance = 1000
            };

            customerAccountsService
                .GetAccountAsync(Arg.Any<string>())
                .Returns(Task.FromResult(accountResponse));

            // Act
            var actionResult = await accountsController.GetAccountByIban(accountIban);
            var objectResult = actionResult as OkObjectResult;

            // Assert
            Assert.NotNull(objectResult);
            Assert.Equal(objectResult.StatusCode, (int)System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAccountByIban_WithAccountIbanNotExists_ReturnStatusCodeNotFound()
        {
            // Arrange
            var accountIban = "BBBB";

            customerAccountsService
                .GetAccountAsync(Arg.Any<string>())
                .Returns(Task.FromResult((AccountResponse)null));

            // Act
            var actionResult = await accountsController.GetAccountByIban(accountIban);

            // Assert
            var requestResult = Assert.IsType<NotFoundResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.NotFound, requestResult.StatusCode);
        }

        [Fact]
        public async Task CreateAccount_WithCorrectRequest_ReturnStatusCodeOK()
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

            customerAccountsService
                .AccountExistsAsync(Arg.Any<AccountRequest>())
                .Returns(false);

            customerAccountsService
                .CreateAccountAsync(Arg.Any<AccountRequest>())
                .Returns(Task.FromResult(new AccountResponse()));

            // Act
            var actionResult = await accountsController.CreateAccount(accountRequest);

            // Assert
            var requestResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.OK, requestResult.StatusCode);
        }

        [Fact]
        public async Task CreateAccount_WithAccountIbanExists_ReturnStatusCodeBadRequest()
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

            customerAccountsService
                .AccountExistsAsync(Arg.Any<AccountRequest>())
                .Returns(true);

            // Act
            var actionResult = await accountsController.CreateAccount(accountRequest);

            // Assert
            var requestResult = Assert.IsType<BadRequestObjectResult>(actionResult);
            Assert.Equal((int)System.Net.HttpStatusCode.BadRequest, requestResult.StatusCode);
        }

    }
}
