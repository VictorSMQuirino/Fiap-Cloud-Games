using System.Net;
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
        var request = httpContext.Request;
        var method = request.Method;
        var path = request.Path;
        var queryString = request.QueryString.HasValue ? request.QueryString.Value : "";
        object? statusCode = null;

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
            
            statusCode = appException.StatusCode;
            
            httpContext.Response.StatusCode = (int)statusCode;
        }
        else
        {
            response = new
            {
                Code = ErrorCode.UnexpectedError.ToString(),
                exception.Message,
                Timestamp = DateTime.UtcNow
            };

            statusCode = HttpStatusCode.InternalServerError;
        }
        
        _logger.LogError(
            "Error in {Method} {Path}{QueryString} => Status code: {StatusCode}. Message: {Message}",
            method,
            path,
            queryString,
            (int)statusCode,
            exception.Message);
        
        httpContext.Response.ContentType = "application/json";
        var json = JsonSerializer.Serialize(response);
        await httpContext.Response.WriteAsync(json, cancellationToken);

        return true;
    }
}