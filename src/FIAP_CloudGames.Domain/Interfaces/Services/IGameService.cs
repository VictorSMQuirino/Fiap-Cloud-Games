using FIAP_CloudGames.Domain.DTO;

namespace FIAP_CloudGames.Domain.Interfaces.Services;

public interface IGameService
{
    Task<Guid> CreateAsync(CreateGameDto gameDto);
    Task UpdateAsync(Guid id, UpdateGameDto gameDto);
    Task DeleteAsync(Guid id);
    Task<GameDto?> GetByIdAsync(Guid id);
    Task<ICollection<GameDto>> GetAllAsync();
}