namespace FIAP_CloudGames.Domain.Entities;

public class Promotion : BaseEntity
{
    public Guid GameId { get; set; }
    public Game? Game { get; set; }
    public int DiscountPercentage { get; set; }
    public DateOnly Deadline { get; set; }
    public bool Active { get; set; }
}