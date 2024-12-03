using api.domain;
using api.DTO;

namespace api.application;

public interface IApplicationRepository
{
    Task<ApiResponseDTO> GetAllUsers();

    Task CreateUser(User user, string password);

    Task<User> GetUserById(string Id);

    Task<User> GetUserByEmail(string email);

    Task<List<User>> GetUserByName(string name);

    Task DeleteUser(string Id);

    Task<ApiResponseDTO> Login(LoginDTO request);

    Task<ApiResponseDTO> Register(RegistrationDTO request);

} 