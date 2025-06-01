using System.Diagnostics;

namespace FIAP_CloudGames.API.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingMiddleware> _logger;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();
        
        var request = context.Request;
        var method = request.Method;
        var path = request.Path;
        var queryString = request.QueryString.HasValue ? request.QueryString.Value : "";
        
        var userName = (bool)context.User.Identity?.IsAuthenticated
            ? context.User.Identity.Name 
            : "Anonymus";
        
        _logger.LogInformation(
            "Starting: {Method} {Path} {QueryString}",
            method,
            path,
            queryString
            );
        
        await _next(context);
            
        var statusCode = context.Response.StatusCode;
        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            
        _logger.LogInformation(
            "Finished: {Method} {Path} {QueryString} => {StatusCode} in {ElapsedMilliseconds} ms",
            method,
            path,
            queryString,
            statusCode,
            elapsedMilliseconds
        );
    }
}