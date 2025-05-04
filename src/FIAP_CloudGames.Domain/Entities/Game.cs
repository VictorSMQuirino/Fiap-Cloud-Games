namespace FIAP_CloudGames.Domain.Entities;

public class Game : BaseEntity
{
    public string Title { get; set; }
    public decimal Price { get; set; }
    public DateOnly ReleaseDate { get; set; }
    public ICollection<UserGame> UserGames { get; set; }
}
