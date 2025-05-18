using FIAP_CloudGames.API.Requests.Promotion;
using FIAP_CloudGames.API.Responses.Promotion;
using FIAP_CloudGames.Domain.DTO.Promotion;

namespace FIAP_CloudGames.API.Extensions.Converters.Promotion;

public static class PromotionConverter
{
    public static CreatePromotionDto ToDto(this CreatePromotionRequest request)
        => new(request.GameId, request.DiscountPercentage, request.Deadline);
    
    public static UpdatePromotionDto ToDto(this UpdatePromotionRequest request) 
        => new(request.GameId, request.DiscountPercentage, request.Deadline);

    public static GetPromotionByIdResponse ToResponse(this PromotionDto dto)
        => new(dto.Id, dto.Game, dto.DiscountPercentage, dto.Deadline, dto.Active);
    
    public static ICollection<GetPromotionByIdResponse> ToResponse(this ICollection<PromotionDto> dtoList) 
        => dtoList.Select(ToResponse).ToList();
}
