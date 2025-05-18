namespace FIAP_CloudGames.API.Requests.Promotion;

public record CreatePromotionRequest(Guid GameId, int DiscountPercentage, DateOnly Deadline);