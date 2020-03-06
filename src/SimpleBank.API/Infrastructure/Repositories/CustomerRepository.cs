using SimpleBank.API.Infrastructure.Data;
using SimpleBank.API.Infrastructure.Repositories.Interfaces;
using SimpleBank.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public CustomerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Customer> GetCustomerAsync(string firstname, string lastname)
        {
            return await dbContext.Customers.FirstOrDefaultAsync(x => x.FirstName == firstname && x.LastName == lastname);
        }

        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<bool> CustomerExistsAsync(string firstname, string lastname, string email)
        {
            return await dbContext.Customers.AnyAsync(x => x.FirstName == firstname && x.LastName == lastname && x.Email == email);
        }

    }
}
