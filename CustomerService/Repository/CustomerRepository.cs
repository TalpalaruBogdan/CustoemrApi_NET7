using CustomerService.DTOs;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Models;

namespace CustomerService.Repository
{
    public class CustomerRepository : ICustomerRepository
    {

        private CustomerContext _customerContext;

        public CustomerRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
            => await _customerContext.Customers.ToListAsync();

        public async Task<Customer?> GetCustomer(Guid id)
            => await _customerContext.Customers.FirstOrDefaultAsync(x => x.Id == id);

        public async Task CreateCustomer(Customer customer)
        {
            var customerToAdd = await _customerContext.Customers.FirstOrDefaultAsync(x => x.Id == customer.Id);
            if (customerToAdd is null)
            {
                await _customerContext.AddAsync(customer);
                await _customerContext.SaveChangesAsync();
            }
        }

        public async Task DeleteCustomer(Guid id)
        {
            var customerToRemove = await _customerContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customerToRemove != null)
            {
                _customerContext.Remove(customerToRemove);
                await _customerContext.SaveChangesAsync();
            }
        }

        public async Task UpdateCustomer(Guid id, CustomerDTO customerDTO)
        {
            var customerToModify = await _customerContext.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (customerToModify != null)
            {
                customerToModify.City = customerDTO.City;
                customerDTO.Country = customerDTO.Country;
                customerDTO.LastName = customerDTO.LastName;
                customerDTO.FirstName = customerDTO.FirstName;
                customerDTO.Address = customerDTO.Address;
                await _customerContext.SaveChangesAsync();
            }
        }

    }
}