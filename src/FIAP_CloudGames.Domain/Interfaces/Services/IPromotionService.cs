using FIAP_CloudGames.Domain.DTO.Promotion;

namespace FIAP_CloudGames.Domain.Interfaces.Services;

public interface IPromotionService
{
    Task<Guid> CreateAsync(CreatePromotionDto dto);
    Task UpdateAsync(Guid id, UpdatePromotionDto dto);
    Task DeleteAsync(Guid id);
    Task<PromotionDto?> GetByIdAsync(Guid id);
    Task<ICollection<PromotionDto>> GetAllAsync();
    Task<ICollection<PromotionDto>> GetAllActiveAsync();
    Task ActivePromotionAsync(Guid id);
    Task DeactivePromotionAsync(Guid id);
}