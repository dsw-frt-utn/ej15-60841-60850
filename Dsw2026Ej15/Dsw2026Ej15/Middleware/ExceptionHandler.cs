using Dsw2026Ej15.Domain.Exceptions;
namespace Dsw2026Ej15.Api.Middleware;

public static class ExceptionHandler
{
    public static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        if (ex is ValidationException ve)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync($"{{\"error\": \"{ve.Message}\"}}");
        }
        else
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("{\error\": \"Problem\"}");
        }
    }
}
