using DadosAplicativo.Api.DTOs.Requests;
using DadosAplicativo.Api.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace DadosAplicativo.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var (statusCode, response) = await _authService.LoginAsync(request);
        return StatusCode(statusCode, response);
    }
}
