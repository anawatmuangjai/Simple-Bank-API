using SimpleBank.API.Infrastructure.Data;
using SimpleBank.API.Infrastructure.Repositories.Interfaces;
using SimpleBank.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> GetUserAsync(string username, string password)
        {
            return await dbContext.Users.FirstOrDefaultAsync(x => x.Username == username && x.Password == password);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await dbContext.Users.AnyAsync(x => x.Username == username);
        }

        public async Task<User> AddUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }
    }
}
