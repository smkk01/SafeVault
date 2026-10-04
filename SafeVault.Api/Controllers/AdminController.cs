using Microsoft.AspNetCore.Mvc;

namespace SafeVault.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    [HttpGet]
    public IActionResult Dashboard(
        [FromQuery] string role)
    {
        if (role != "Admin")
        {
            return StatusCode(StatusCodes.Status403Forbidden);
        }

        return Ok(
            "Welcome to Admin Dashboard");
    }
}