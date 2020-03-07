using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleBank.API.Infrastructure.Services.Interfaces;
using SimpleBank.API.Models.Dtos;

namespace SimpleBank.API.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [Authorize]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService userService;

        public UsersController(IUsersService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticationRequest request)
        {
            var response = await userService.AuthenticateAsync(request.Username, request.Password);

            if (response == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticationRequest request)
        {
            var isExists = await userService.UserExistsAsync(request.Username);

            if (isExists)
            {
                return BadRequest(new { message = "Username already exists" });
            }

            var response = await userService.RegisterAsync(request.Username, request.Password);

            if (response == null)
            {
                return BadRequest(new { message = "Error when registering" });
            }

            return Ok();
        }
    }
}