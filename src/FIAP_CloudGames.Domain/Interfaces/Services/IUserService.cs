using FIAP_CloudGames.Domain.DTO.User;
using FIAP_CloudGames.Domain.Enums;

namespace FIAP_CloudGames.Domain.Interfaces.Services;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id);
    Task<ICollection<UserDto>> GetAllAsync();
    Task ChangeRoleAsync(Guid id);
}