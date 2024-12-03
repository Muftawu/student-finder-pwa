using api.domain;
using api.application;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using api.DTO;

namespace api.infrastructure;

public class ApplicationRepository : IApplicationRepository
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtService _jwtService;

    public ApplicationRepository(UserManager<User> userManager, IJwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<ApiResponseDTO> GetAllUsers()
    {
        var users = await _userManager.Users!.ToListAsync();
        if (users == null) 
        {
            return new ApiResponseDTO
            {
                Success = false
            };
        }
        return new ApiResponseDTO
        {
            Success = true,
            Users = users,

        };
    }

    public async Task CreateUser(User user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task<List<User>> GetUserByName(string name)
    {
        return await _userManager.Users.Where(u => u.FirstName.Contains(name)).ToListAsync();
    }

    public async Task<User> GetUserByEmail(string email)
    {
        return await _userManager.FindByEmailAsync(email) ?? throw new Exception($"User with email {email} not found");
    }

    public async Task<User> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id) ?? throw new Exception($"User with ID {id} not found");
    }

    public async Task DeleteUser(string id)
    {
        var user = await GetUserById(id);
        var result = await _userManager.DeleteAsync(user);
        if (!result.Succeeded)
        {
            throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
        }
    }

    public async Task<ApiResponseDTO> Login(LoginDTO request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email!);
        
        if (user == null)
        {
            return new ApiResponseDTO 
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password ?? string.Empty);
        
        if (!isPasswordValid)
        {
            return new ApiResponseDTO 
            {
                Success = false,
                Message = "Invalid credentials"
            };
        }

        return new ApiResponseDTO
        {
            Success = true,
            Message = "Login Successful",
            Token = _jwtService.GenerateToken(user)
        };
    }

    public async Task<ApiResponseDTO> Register(RegistrationDTO request)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email!);
        if (existingUser != null)
        {
            return new ApiResponseDTO
            {
                Success = false,
                Message = "User with this email already exists"
            };
        }        

        var user = new User
        {
            UserName = request.Email,
            FirstName = request.FirstName!,
            LastName = request.LastName!,
            Email = request.Email!,
            PasswordHash = request.Password!
        };

        var result = await _userManager.CreateAsync(user, request.Password!);

        if (result.Succeeded)
        {
            return new ApiResponseDTO
            {
                Success = true,
                Message = "Registration successful. Please log in."
            };
        }

        return new ApiResponseDTO
        {
            Success = false,
            Message = "User registration failed",
            Errors = result.Errors.Select(e => e.Description)
        };
    }


}