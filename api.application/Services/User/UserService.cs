using api.domain;
using api.DTO;

namespace api.application;

public class UserService : IUserService
{
    private readonly IApplicationRepository _applicationRepository; 

    public UserService(IApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public Task<ApiResponseDTO> GetAllUsers()
    {
        return _applicationRepository.GetAllUsers();
    }

    public Task CreateUser(User user, string password)
    {
        return _applicationRepository.CreateUser(user, password);
    }

    public Task<List<User>> GetUserByName(string name)
    {
        return _applicationRepository.GetUserByName(name);
    }

    public Task<User> GetUserByEmail(string email)
    {
        return _applicationRepository.GetUserByEmail(email);
    }

    public Task<User> GetUserById(string id)
    {
        return _applicationRepository.GetUserById(id);
    }

    public Task DeleteUser(string id)
    {
        return _applicationRepository.DeleteUser(id);
    }

    public Task<ApiResponseDTO> Login(LoginDTO request)
    {
       return _applicationRepository.Login(request);
    }

    public Task<ApiResponseDTO> Register(RegistrationDTO request)
    {
        return _applicationRepository.Register(request);
    }
}