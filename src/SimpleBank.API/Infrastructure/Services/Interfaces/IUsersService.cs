using SimpleBank.API.Models.Dtos;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Services.Interfaces
{
    public interface IUsersService
    {
        Task<AuthenticationResponse> AuthenticateAsync(string username, string password);
        Task<AuthenticationResponse> RegisterAsync(string username, string password);
        Task<bool> UserExistsAsync(string username);
    }
}
