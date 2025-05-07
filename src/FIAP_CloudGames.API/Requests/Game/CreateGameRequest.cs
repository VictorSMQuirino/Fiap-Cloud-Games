namespace FIAP_CloudGames.API.Requests.Game;

public record CreateGameRequest(string Title, decimal Price, DateOnly ReleaseDate);