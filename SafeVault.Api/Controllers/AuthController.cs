using Microsoft.AspNetCore.Mvc;
using SafeVault.Api.Models;
using SafeVault.Api.Services;

namespace SafeVault.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserService _userService;
    private readonly AuthService _authService;

    public AuthController(
        UserService userService,
        AuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }

    [HttpPost("register")]
public IActionResult Register(
    [FromBody] RegisterRequest request)
    {
        string hash =
            _authService.HashPassword(
                request.Password);

        _userService.RegisterUser(
            request.Username,
            request.Email,
            hash,
            request.Role);

        return Ok("Registration successful");
    }

    [HttpPost("login")]
    public IActionResult Login(
        LoginRequest request)
    {
        var user =
            _userService.GetUserByUsername(
                request.Username);

        if (user == null)
            return Unauthorized();

        bool validPassword =
            _authService.VerifyPassword(
                request.Password,
                user.PasswordHash);

        if (!validPassword)
            return Unauthorized();

        return Ok(new
        {
            Message = "Login Successful",
            Username = user.Username,
            Role = user.Role
        });
    }
}