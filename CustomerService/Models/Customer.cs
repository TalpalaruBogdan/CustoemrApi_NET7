using CustomerService.DTOs;

namespace Models
{
    public class Customer : CustomerDTO
    {
        public Guid Id { get; set; }
    }
}