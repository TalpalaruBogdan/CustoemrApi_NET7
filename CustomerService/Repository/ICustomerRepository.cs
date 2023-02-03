using CustomerService.DTOs;
using Models;

namespace CustomerService.Repository
{
    public interface ICustomerRepository
    {
        Task CreateCustomer(Customer customer);

        Task DeleteCustomer(Guid id);

        Task<Customer?> GetCustomer(Guid id);

        Task<IEnumerable<Customer>> GetCustomers();

        Task UpdateCustomer(Guid id, CustomerDTO customerDTO);
    }
}