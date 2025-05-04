using System.Net;
using FIAP_CloudGames.Domain.Enums;

namespace FIAP_CloudGames.Domain.Exceptions;

public class ValidationException : AppException
{
    public ValidationException(IDictionary<string, string[]> errors) 
        : base("One or more validation errors occurred", ErrorCode.ValidationError)
    {
        Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; set; }
}
