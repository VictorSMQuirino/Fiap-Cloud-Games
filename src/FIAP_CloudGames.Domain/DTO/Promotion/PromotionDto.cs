namespace FIAP_CloudGames.Domain.DTO.Promotion;

public record PromotionDto(Guid Id, GameDto Game, int DiscountPercentage, DateOnly Deadline, bool Active);