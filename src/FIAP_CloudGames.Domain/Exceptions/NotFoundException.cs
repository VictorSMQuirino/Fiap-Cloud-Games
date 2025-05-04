using System.Net;
using FIAP_CloudGames.Domain.Enums;

namespace FIAP_CloudGames.Domain.Exceptions;

public class NotFoundException : AppException
{
    public NotFoundException(string entityName, object? key) 
        : base($"{entityName} not found.", ErrorCode.NotFound, HttpStatusCode.NotFound)
    {
        EntityName = entityName;
        Key = key;
    }

    public string EntityName { get; set; }
    public object? Key { get; set; }
}