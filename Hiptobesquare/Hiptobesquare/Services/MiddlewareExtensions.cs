namespace Hiptobesquare.Services;

public static class MiddlewareExtensions
{
    public static void UseLoggingMiddleware(this WebApplication app) =>
        app.Use(async (context, next) =>
        {
            context.RequestServices.GetRequiredService<ILogger<Program>>()
                .LogInformation($"Request {context.Request.Method} {context.Request.Path} from {context.Connection.RemoteIpAddress}");
            await next();
        });
    
    public static void UseExceptionHandlerMiddleware(this WebApplication app) =>
        app.Use(async (context, next) =>
        {
            try
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    context.RequestServices.GetRequiredService<ILogger<Program>>()
                        .LogWarning($"404 Not Found: {context.Request.Method} {context.Request.Path}");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { message = "The requested resource was not found." });
                }
            }
            catch (Exception ex)
            {
                context.RequestServices.GetRequiredService<ILogger<Program>>()
                    .LogError(ex, "An error occurred");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An error occurred");
            }
        });
}