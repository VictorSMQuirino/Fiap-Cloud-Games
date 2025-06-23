using FIAP_CloudGames.Domain.Interfaces.Services;

namespace FIAP_CloudGames.Application.Services;

public class BcryptService : IPasswordService
{
    public string HashPassword(string password) 
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyHashedPassword(string hashedPassword, string providedPassword) 
        => BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
}