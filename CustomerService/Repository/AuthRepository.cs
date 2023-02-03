using AutoMapper;
using CustomerService.DTOs;
using Infrastructure;

namespace CustomerService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CustomerContext customerContext;
        private readonly IMapper mapper;

        public AuthRepository(CustomerContext customerContext, IMapper mapper)
        {
            this.customerContext = customerContext;
            this.mapper = mapper;
        }

        public bool IsUniqueUser(string userName)
        {
            return customerContext.Users.Any(u => u.UserName == userName);
        }

        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> Register(RegistrationRequestDTO registrationrequest)
        {
            throw new NotImplementedException();
        }
    }
}
