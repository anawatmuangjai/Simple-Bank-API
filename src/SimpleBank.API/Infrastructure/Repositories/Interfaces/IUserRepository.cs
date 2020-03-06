using SimpleBank.API.Models;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserAsync(string username, string password);
        Task<User> AddUserAsync(User user);
        Task<bool> UserExistsAsync(string username);
    }
}
