using FIAP_CloudGames.API.Requests.Auth;
using FIAP_CloudGames.Domain.DTO.Auth;

namespace FIAP_CloudGames.API.Extensions.Converters.Auth;

public static class AuthConverter
{
    public static LoginDto ToDto(this LoginRequest request) 
        => new(request.Email, request.Password);
}