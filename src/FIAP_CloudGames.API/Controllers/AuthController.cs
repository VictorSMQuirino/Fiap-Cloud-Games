using FIAP_CloudGames.API.Extensions.Converters.User;
using FIAP_CloudGames.API.Requests.User;
using FIAP_CloudGames.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FIAP_CloudGames.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService, IConfiguration configuration)
    {
        _authService = authService;
        _configuration = configuration;
    }

    [HttpPost("signup")]
    public async Task<ActionResult> SignUp([FromBody] CreateUserRequest request)
    {
        var dto = request.ToDto();
        
        await _authService.SignUp(dto, _configuration);

        return Created();
    }
}