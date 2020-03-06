using SimpleBank.API.Models;
using System.Threading.Tasks;

namespace SimpleBank.API.Infrastructure.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerAsync(string firstname, string lastname);
        Task<Customer> AddCustomerAsync(Customer user);
        Task<bool> CustomerExistsAsync(string firstname, string lastname, string email);
    }
}
