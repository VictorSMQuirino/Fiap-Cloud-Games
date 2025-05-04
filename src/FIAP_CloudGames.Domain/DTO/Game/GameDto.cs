namespace FIAP_CloudGames.Domain.DTO;

public record GameDto(Guid Id, string Title, decimal Price, DateOnly ReleaseDate);