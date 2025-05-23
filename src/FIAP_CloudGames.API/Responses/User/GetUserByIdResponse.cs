namespace FIAP_CloudGames.API.Responses.User;

public record GetUserByIdResponse(Guid Id, string Name, string Email);