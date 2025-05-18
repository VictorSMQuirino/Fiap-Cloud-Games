using FIAP_CloudGames.Domain.DTO;

namespace FIAP_CloudGames.API.Responses.Promotion;

public record GetPromotionByIdResponse(Guid Id, GameDto Game, int DiscountPercentage, DateOnly Deadline, bool Active);