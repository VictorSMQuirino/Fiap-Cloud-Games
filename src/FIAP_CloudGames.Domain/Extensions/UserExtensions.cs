using FIAP_CloudGames.Domain.DTO.User;
using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Enums;

namespace FIAP_CloudGames.Domain.Extensions;

public static class UserExtensions
{
    public static User ToUser(this CreateUserDto dto, string hashedPassword) 
        => new()
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = hashedPassword,
            Role = UserRole.Common
        };

    public static UserDto ToDto(this User user)
        => new(user.Id, user.Name, user.Email);

    public static ICollection<UserDto> ToDtoList(this IEnumerable<User> users)
        => users.Select(u => u.ToDto()).ToList();
}