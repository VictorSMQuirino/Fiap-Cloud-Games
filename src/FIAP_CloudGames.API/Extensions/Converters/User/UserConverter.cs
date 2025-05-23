using FIAP_CloudGames.API.Requests.User;
using FIAP_CloudGames.API.Responses.User;
using FIAP_CloudGames.Domain.DTO.User;

namespace FIAP_CloudGames.API.Extensions.Converters.User;

public static class UserConverter
{
    public static CreateUserDto ToDto(this CreateUserRequest request) 
        => new(request.Name, request.Email, request.Password);
    
    public static GetUserByIdResponse ToResponse(this UserDto dto) 
        => new(dto.Id, dto.Name, dto.Email);
    
    public static ICollection<GetUserByIdResponse> ToResponse(this ICollection<UserDto> dtoList) 
        => dtoList.Select(ToResponse).ToList();
}