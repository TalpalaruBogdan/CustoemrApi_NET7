using AutoMapper;
using CustomerService.DTOs;
using Models;

namespace CustomerService.ExtensionMethods
{
    public static class CustomerExtensions
    {
        public static List<CustomerDTO> ToCustomerDtoList(this List<Customer> customers, IMapper mapper)
        {
            var customerDtoList = new List<CustomerDTO>();
            if (customers == null)
            {
                return customerDtoList;
            }
            customers.ForEach(customer =>
            {
                customerDtoList.Add(mapper.Map<CustomerDTO>(customer));
            });
            return customerDtoList;
        }
    }
}
