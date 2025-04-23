namespace FIAP_CloudGames.Domain.Entities;

public class Game : BaseEntity
{
    public string Title { get; set; }
    public decimal Price { get; set; }
}
