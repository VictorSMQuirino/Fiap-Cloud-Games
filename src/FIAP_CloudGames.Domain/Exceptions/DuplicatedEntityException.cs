using System.Net;

namespace FIAP_CloudGames.Domain.Exceptions;

public class DuplicatedEntityException : DomainException
{
    public DuplicatedEntityException(string entityName, string propertyName, object attemptedValue) 
        : base(
            $"Already exists a {entityName} with {propertyName} equals to '{attemptedValue}'.", 
            entityName, 
            propertyName, 
            attemptedValue,
            HttpStatusCode.Conflict) { }
}