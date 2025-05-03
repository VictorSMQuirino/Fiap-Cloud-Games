using FIAP_CloudGames.Domain.Enums;

namespace FIAP_CloudGames.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public ICollection<UserGame> UserGames { get; set; }
}
