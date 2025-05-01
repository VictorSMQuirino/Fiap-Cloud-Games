namespace FIAP_CloudGames.Domain.Entities;

public class UserGame : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid GameId { get; set; }
}