using CustomerService.DTOs;

namespace CustomerService.Repository
{
    public interface IAuthRepository
    {
        public bool IsUniqueUser(string userName);
        public Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        public Task<UserDTO> Register(RegistrationRequestDTO registrationrequest);
    }
}
