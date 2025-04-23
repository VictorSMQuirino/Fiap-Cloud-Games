namespace FIAP_CloudGames.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Game> Games { get; set; } = default!;
}
