using System.Text.Json;
using FIAP_CloudGames.Domain.Enums;
using FIAP_CloudGames.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace FIAP_CloudGames.API.Middlewares;

public class ExceptionHandlingMiddleware : IExceptionHandler
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);

        object? response;
        
        if (exception is AppException appException)
        {
            object? details = appException switch
            {
                DomainException ex => new { ex.EntityName, ex.PropertyName, ex.AttemptedValue },
                ValidationException ex => ex.Errors,
                NotFoundException ex => new { ex.EntityName, ex.Key },
                _ => null
            };

            response = new
            {
                Code = appException.ErrorCode.ToString(),
                appException.Message,
                Details = details,
                Timestamp = DateTime.UtcNow
            };
            
            httpContext.Response.StatusCode = (int)appException.StatusCode;
        }
        else
        {
            response = new
            {
                Code = ErrorCode.UnexpectedError.ToString(),
                exception.Message,
                Timestamp = DateTime.UtcNow
            };
        }
        
        httpContext.Response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(response);
        await httpContext.Response.WriteAsync(json, cancellationToken);

        return true;
    }
}