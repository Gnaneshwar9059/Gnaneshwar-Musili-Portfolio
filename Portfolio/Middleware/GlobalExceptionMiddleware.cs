using System.Text.Json;

namespace Portfolio.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception)
        {
            context.Response.StatusCode = 500;

            context.Response.ContentType = "application/json";

            var response = new
            {
                success = false,
                message = "Something went wrong. Please try again later."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}