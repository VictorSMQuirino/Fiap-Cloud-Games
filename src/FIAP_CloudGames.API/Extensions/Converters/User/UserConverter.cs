using FIAP_CloudGames.API.Requests.User;
using FIAP_CloudGames.Domain.DTO.User;

namespace FIAP_CloudGames.API.Extensions.Converters.User;

public static class UserConverter
{
    public static CreateUserDto ToDto(this CreateUserRequest request) 
        => new(request.Name, request.Email, request.Password);
}