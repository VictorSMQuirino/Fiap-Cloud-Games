using FIAP_CloudGames.Domain.Entities;
using FIAP_CloudGames.Domain.Interfaces.Repositories;
using FIAP_CloudGames.Infrastructure.Context;

namespace FIAP_CloudGames.Infrastructure.Repositories;

public class GameRepository : BaseRepository<Game>, IGameRepository
{
    public GameRepository(FiapCloudGamesDbContext context) : base(context) { }
}