using FIAP_CloudGames.Domain.Entities;

namespace FIAP_CloudGames.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}