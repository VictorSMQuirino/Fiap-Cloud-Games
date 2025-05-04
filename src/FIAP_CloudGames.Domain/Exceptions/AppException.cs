using System.Net;
using FIAP_CloudGames.Domain.Enums;

namespace FIAP_CloudGames.Domain.Exceptions;

public abstract class AppException : Exception
{
    protected AppException(string message, ErrorCode errorCode, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
        : base(message)
    {
       ErrorCode = errorCode;
       StatusCode = statusCode;
    }
    
    public ErrorCode ErrorCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }
}
