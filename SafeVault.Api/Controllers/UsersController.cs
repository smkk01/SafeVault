using Microsoft.AspNetCore.Mvc;
using SafeVault.Api.Services;
using SafeVault.Api.Utilities;

namespace SafeVault.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _service;

    public UsersController(UserService service)
    {
        _service = service;
    }

    [HttpPost]
    [HttpPost("CreateUser")]  
    public IActionResult CreateUser(
        [FromBody] CreateUserRequest request)
    {
        var username =
            InputValidator.SanitizeInput(
                request.Username);

        var email =
            InputValidator.SanitizeInput(
                request.Email);

        if (!InputValidator.IsValidEmail(email))
            return BadRequest("Invalid Email");

        _service.AddUser(username, email);

        return Ok("User Created");
    }

    [HttpGet("{username}")]
    public IActionResult GetUser(
        string username)
    {
        var user =
            _service.GetUser(username);

        if (user == null)
            return NotFound();

        return Ok(user);
    }
}