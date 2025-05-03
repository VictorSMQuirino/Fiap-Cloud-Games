namespace FIAP_CloudGames.Domain.Entities;

public class UserGame : BaseEntity
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
}