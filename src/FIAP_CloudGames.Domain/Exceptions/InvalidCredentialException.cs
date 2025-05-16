using System.Net;
using FIAP_CloudGames.Domain.Enums;

namespace FIAP_CloudGames.Domain.Exceptions;

public class InvalidCredentialException : AppException
{
    public InvalidCredentialException() 
        : base("Invalid email or password.", ErrorCode.InvalidCredentialsError, HttpStatusCode.Unauthorized) { }
}
