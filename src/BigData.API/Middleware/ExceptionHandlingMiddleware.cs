using System.Net;
using System.Text.Json;
using BigData.Core.Exceptions;

namespace BigData.API.Middleware;

/// <summary>
/// Global exception handling middleware
/// </summary>
public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "An error occurred: {Message}", exception.Message);

        var response = context.Response;
        response.ContentType = "application/json";

        object errorResponse;
        response.StatusCode = (int)HttpStatusCode.InternalServerError;

        switch (exception)
        {
            case NotFoundException notFoundException:
                response.StatusCode = (int)HttpStatusCode.NotFound;
                errorResponse = new
                {
                    message = notFoundException.Message,
                    statusCode = (int)HttpStatusCode.NotFound
                };
                break;

            case ValidationException validationException:
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse = new
                {
                    message = "Validation failed",
                    statusCode = (int)HttpStatusCode.BadRequest,
                    errors = validationException.Errors
                };
                break;

            case UnauthorizedException unauthorizedException:
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse = new
                {
                    message = unauthorizedException.Message,
                    statusCode = (int)HttpStatusCode.Unauthorized
                };
                break;

            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse = new
                {
                    message = "An internal server error occurred",
                    statusCode = (int)HttpStatusCode.InternalServerError
                };
                break;
        }

        var result = JsonSerializer.Serialize(errorResponse);
        await response.WriteAsync(result);
    }
}
