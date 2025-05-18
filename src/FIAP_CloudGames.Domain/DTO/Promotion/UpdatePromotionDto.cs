namespace FIAP_CloudGames.Domain.DTO.Promotion;

public record UpdatePromotionDto(Guid GameId, int DiscountPercentage, DateOnly Deadline);