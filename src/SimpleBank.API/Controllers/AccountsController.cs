using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBank.API.Infrastructure.Services.Interfaces;
using SimpleBank.API.Models.Dtos;

namespace SimpleBank.API.Controllers
{
    [Route("api/v{version:apiVersion}/accounts")]
    [Authorize]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ICustomerAccountsService customerAccountsService;

        public AccountsController(ICustomerAccountsService customerAccountsService)
        {
            this.customerAccountsService = customerAccountsService;
        }

        /// <summary>
        /// Get individual account by IBAN
        /// </summary>
        /// <param name="accountIban"></param>
        /// <returns></returns>
        [HttpGet("{accountIban}", Name = "GetAccountByIban")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AccountResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> GetAccountByIban(string accountIban)
        {
            var accountResponse = await customerAccountsService.GetAccountAsync(accountIban);

            if (accountResponse == null)
            {
                return NotFound();
            }

            return Ok(accountResponse);
        }

        /// <summary>
        /// Register new account
        /// </summary>
        /// <param name="accountRequest"></param>
        /// <returns></returns>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(AccountResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAccount([FromBody] AccountRequest accountRequest)
        {
            if (accountRequest == null)
            {
                return BadRequest(ModelState);
            }

            var accountExists = await customerAccountsService.AccountExistsAsync(accountRequest);

            if (accountExists)
            {
                ModelState.AddModelError("", "Account exists");
                return StatusCode(404, ModelState);
            }

            var accountResponse = await customerAccountsService.CreateAccountAsync(accountRequest);

            return Ok(accountResponse);
        }
    }
}