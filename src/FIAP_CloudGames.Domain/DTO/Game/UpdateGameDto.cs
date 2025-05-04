namespace FIAP_CloudGames.Domain.DTO;

public record UpdateGameDto(string Title, decimal Price, DateOnly ReleaseDate);