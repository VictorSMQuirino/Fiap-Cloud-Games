using FIAP_CloudGames.Domain.DTO;
using FIAP_CloudGames.Domain.Entities;

namespace FIAP_CloudGames.Domain.Extensions;

public static class GameExtensions
{
    public static Game ConvertDtoToGame(this CreateGameDto gameDto) 
        => new()
        {
            Title = gameDto.Title,
            Price = gameDto.Price,
            ReleaseDate = gameDto.ReleaseDate
        };

    public static Game ConvertDtoToGame(this UpdateGameDto gameDto, Game game)
    {
        game.Title = gameDto.Title;
        game.Price = gameDto.Price;
        game.ReleaseDate = gameDto.ReleaseDate;
        
        return game;
    }

    public static GameDto ToGameDto(this Game game) 
        => new(game.Id, game.Title, game.Price, game.ReleaseDate);

    public static List<GameDto> ToGameDtoList(this IEnumerable<Game> games) 
        => games.Select(g => g.ToGameDto()).ToList();
    
}