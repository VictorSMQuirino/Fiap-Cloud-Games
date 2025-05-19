using FIAP_CloudGames.Domain.Entities;

namespace FIAP_CloudGames.Domain.Interfaces.Repositories;

public interface IPromotionRepository : IBaseRepository<Promotion>
{
    Task<ICollection<Promotion>> GetAllActivePromotions();
}