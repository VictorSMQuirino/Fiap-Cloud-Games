using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Infrastructure.Context;

namespace FIAP_CloudGames.Infrastructure.Repositories;

public class PromotionRepository : BaseRepository<Promotion>, IPromotionRepository
{
    public PromotionRepository(FiapCloudGamesDbContext context) : base(context) { }

    public async Task<ICollection<Promotion>> GetAllActivePromotions() 
        => await GetListBy(p => p.Active, p => p.Game);
}