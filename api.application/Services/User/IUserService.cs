using api.domain;
using api.DTO;

namespace api.application;

public interface IUserService
{
    Task<ApiResponseDTO> GetAllUsers();

    Task CreateUser(User user, string password);

    Task<User> GetUserById(string id);

    Task<User> GetUserByEmail(string email);

    Task<List<User>> GetUserByName(string name);

    Task DeleteUser(string id);

    Task<ApiResponseDTO> Login (LoginDTO request);

    Task<ApiResponseDTO> Register(RegistrationDTO request);

} 