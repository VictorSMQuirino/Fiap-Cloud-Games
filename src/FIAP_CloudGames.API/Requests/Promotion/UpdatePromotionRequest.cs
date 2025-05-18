namespace FIAP_CloudGames.API.Requests.Promotion;

public record UpdatePromotionRequest(Guid GameId, int DiscountPercentage, DateOnly Deadline);