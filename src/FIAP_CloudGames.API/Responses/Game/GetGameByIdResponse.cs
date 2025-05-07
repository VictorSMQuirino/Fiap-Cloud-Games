namespace FIAP_CloudGames.API.Responses.Game;

public record GetGameByIdResponse(Guid Id, string Title, decimal Price, DateOnly ReleaseDate);