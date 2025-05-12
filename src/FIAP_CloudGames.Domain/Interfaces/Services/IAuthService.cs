using FIAP_CloudGames.Domain.DTO.User;
using Microsoft.Extensions.Configuration;

namespace FIAP_CloudGames.Domain.Interfaces.Services;

public interface IAuthService
{
    Task SignUp(CreateUserDto dto, IConfiguration configuration);
    // Task Login(string username, string password);
}