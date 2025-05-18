namespace FIAP_CloudGames.Domain.DTO.Promotion;

public record CreatePromotionDto(Guid GameId, int DiscountPercentage, DateOnly Deadline);