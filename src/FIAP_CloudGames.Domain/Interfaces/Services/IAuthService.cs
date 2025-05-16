using FIAP_CloudGames.Domain.DTO.Auth;
using FIAP_CloudGames.Domain.DTO.User;
using Microsoft.Extensions.Configuration;

namespace FIAP_CloudGames.Domain.Interfaces.Services;

public interface IAuthService
{
    Task SignUp(CreateUserDto dto, IConfiguration configuration);
    Task<string> Login(LoginDto dto);
}