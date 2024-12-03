using api.application;
using api.domain;
using api.DTO;
using Microsoft.AspNetCore.Mvc;

namespace api.presentation;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO request)
    {
        try
        {
            var token = await _userService.Login(request);
            return Ok(token);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials");
        }
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationDTO request)
    {
        var result = await _userService.Register(request);

        if (result is { Success: false })
        {
            return BadRequest(result); 
        }

        return Ok(result);
    }

    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userService.GetAllUsers();
        if (users == null)
        {
            return NotFound();
        }
        return Ok(users);
    }
    

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] User user, string password)
    {
        if (user == null || string.IsNullOrEmpty(password))
        {
            return BadRequest("User or password cannot be null");
        }

        try
        {
            await _userService.CreateUser(user, password);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, "User created successfully");
        }
        catch
        {
            return StatusCode(500, "An error occurred while creating the user.");
        }
    }


    [HttpGet("by-id/{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        Console.WriteLine($"User Id => {id}");
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("ID cannot be null or empty");
        }

        try
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"No user found with ID: {id}");
            }

            return Ok(user);
        }
        catch
        {
            return StatusCode(500, "An error occurred while retrieving the user.");
        }
    }


    [HttpGet("by-email/{email}")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email cannot be null or empty");
        }

        try
        {
            var user = await _userService.GetUserByEmail(email);
            if (user == null)
            {
                return NotFound($"No user found with email: {email}");
            }

            return Ok(user);
        }
        catch
        {
            return StatusCode(500, "An error occurred while retrieving the user by email.");
        }
    }


    [HttpGet("by-name/{name}")]
    public async Task<IActionResult> GetUserByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return BadRequest("Name cannot be null or empty");
        }

        try
        {
            var users = await _userService.GetUserByName(name);
            if (users == null || !users.Any())
            {
                return NotFound($"No users found with name: {name}");
            }

            return Ok(users);
        }
        catch
        {
            return StatusCode(500, "An error occurred while retrieving users by name.");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return BadRequest("ID cannot be null or empty");
        }

        try
        {
            await _userService.DeleteUser(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound($"No user found with ID: {id}");
        }
        catch
        {
            return StatusCode(500, "An error occurred while deleting the user.");
        }
    }

}
