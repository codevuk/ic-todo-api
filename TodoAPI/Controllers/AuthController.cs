using Microsoft.AspNetCore.Mvc;
using TodoAPI.Services;
using TodoApp.ViewModels;

namespace TodoAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _authService.Register(request);
        
        return Ok();
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces(typeof(string))]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            // Lets not give any information away about our API if they
            // did a bad request
            return BadRequest("Username or password is incorrect");
        }

        try
        {
            var token = await _authService.Login(request);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}