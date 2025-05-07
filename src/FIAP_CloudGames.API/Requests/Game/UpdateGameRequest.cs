namespace FIAP_CloudGames.API.Requests.Game;

public record UpdateGameRequest(string Title, decimal Price, DateOnly ReleaseDate);