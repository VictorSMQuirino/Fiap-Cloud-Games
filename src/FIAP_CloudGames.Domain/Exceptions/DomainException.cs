using System.Net;
using FIAP_CloudGames.Domain.Enums;

namespace FIAP_CloudGames.Domain.Exceptions;

public class DomainException : AppException
{
    public DomainException(
        string message, 
        string? entityName = null, 
        string? propertyName = null, 
        object? attemptedValue = null, 
        HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : base(message, ErrorCode.DomainError, statusCode)
    {
        EntityName = entityName ?? string.Empty;
        PropertyName = propertyName ?? string.Empty;
        AttemptedValue = attemptedValue;
    }

    public string EntityName { get; set; }
    public string PropertyName { get; set; }
    public object? AttemptedValue { get; set; }
}
