using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBank.API.Infrastructure.Services.Interfaces;
using SimpleBank.API.Models.Dtos;

namespace SimpleBank.API.Controllers
{
    [Route("api/v{version:apiVersion}/transactions")]
    [Authorize]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionsService transactionsService;

        public TransactionsController(ITransactionsService transactionsService)
        {
            this.transactionsService = transactionsService;
        }

        /// <summary>
        /// Deposit
        /// </summary>
        /// <param name="depositRequest"></param>
        /// <returns></returns>
        [HttpPost("deposit")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TransactionsResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Deposit([FromBody] DepositRequest depositRequest)
        {
            if (depositRequest == null)
            {
                return BadRequest(ModelState);
            }

            var transactionsResult = await transactionsService.DepositAsync(depositRequest);

            return Ok(transactionsResult);
        }

        /// <summary>
        /// Withdraw
        /// </summary>
        /// <param name="withdrawRequest"></param>
        /// <returns></returns>
        [HttpPost("withdraw")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TransactionsResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest withdrawRequest)
        {
            if (withdrawRequest == null)
            {
                return BadRequest(ModelState);
            }

            var transactionsResult = await transactionsService.WithdrawAsync(withdrawRequest);

            return Ok(transactionsResult);
        }

        /// <summary>
        /// Transfer
        /// </summary>
        /// <param name="transferRequest"></param>
        /// <returns></returns>
        [HttpPost("transfer")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(TransactionsResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Transfer([FromBody] TransferRequest transferRequest)
        {
            if (transferRequest == null)
            {
                return BadRequest(ModelState);
            }

            var transactionsResult = await transactionsService.TransferAsync(transferRequest);

            return Ok(transactionsResult);
        }
    }
}