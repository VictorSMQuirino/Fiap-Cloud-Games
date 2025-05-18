using FIAP_CloudGames.Domain.DTO;
using FIAP_CloudGames.Domain.DTO.Promotion;
using FIAP_CloudGames.Domain.Entities;

namespace FIAP_CloudGames.Domain.Extensions;

public static class PromotionExtensions
{
    public static Promotion ToEntity(this CreatePromotionDto dto) 
        => new()
        {
            GameId = dto.GameId,
            DiscountPercentage = dto.DiscountPercentage,
            Deadline = dto.Deadline,
            Active = false
        };

    public static Promotion ToEntity(this UpdatePromotionDto dto, Promotion promotion)
    {
        promotion.GameId = dto.GameId;
        promotion.DiscountPercentage = dto.DiscountPercentage;
        promotion.Deadline = dto.Deadline;
        
        return promotion;
    }
    
    public static PromotionDto ToDto(this Promotion promotion, GameDto gameDto) 
        => new(promotion.Id, gameDto, promotion.DiscountPercentage, promotion.Deadline, promotion.Active);

    public static List<PromotionDto> ToPromotionDtoList(this IEnumerable<Promotion> promotions) 
        => promotions.Select(p => p.ToDto(p.Game.ToGameDto())).ToList();
}