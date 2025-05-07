using FIAP_CloudGames.API.Requests.Game;
using FIAP_CloudGames.API.Responses.Game;
using FIAP_CloudGames.Domain.DTO;

namespace FIAP_CloudGames.API.Extensions.Converters.Game;

public static class GameConverter
{
    public static CreateGameDto ToCreateGameDto(this CreateGameRequest request) 
        => new(request.Title, request.Price, request.ReleaseDate);
    
    public static UpdateGameDto ToUpdateGameDto(this UpdateGameRequest request) 
        => new(request.Title, request.Price, request.ReleaseDate);

    public static GetGameByIdResponse ToResponse(this GameDto dto) 
        => new(dto.Id, dto.Title, dto.Price, dto.ReleaseDate);

    public static ICollection<GetGameByIdResponse> ToResponse(this ICollection<GameDto> dtoList) 
        => dtoList.Select(ToResponse).ToList();
}