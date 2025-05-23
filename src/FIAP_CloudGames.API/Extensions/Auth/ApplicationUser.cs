using System.Security.Claims;
using FIAP_CloudGames.Domain.Interfaces.Services;

namespace FIAP_CloudGames.API.Extensions.Auth;

public class ApplicationUser : IApplicationUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public ApplicationUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public Guid GetUserId()
    {
        var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        return Guid.TryParse(id, out var guid) ? guid : Guid.Empty;
    }
}