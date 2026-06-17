using Dsw2026Ej15.Domain.Exceptions;
namespace Dsw2026Ej15.Api.Middleware;

public class ValidationMiddleware
{
    public readonly RequestDelegate _next;

    public ValidationMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch(ValidationException ex)
        {
            await ExceptionHandler.HandleExceptionAsync(context, ex);
        }
    }
}
