using SimpleBank.API.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Data
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var user = new User
                {
                    UserId = Guid.NewGuid(),
                    Username = "admin",
                    Password = "admin",
                    Role = "Admin"
                };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
