namespace FIAP_CloudGames.Domain.DTO;

public record CreateGameDto(string Title, decimal Price, DateOnly ReleaseDate);