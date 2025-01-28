namespace Hiptobesquare.Services;

public static class MiddlewareExtensions
{
    public static void UseLoggingMiddleware(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogInformation($"Request {context.Request.Method} {context.Request.Path} from {context.Connection.RemoteIpAddress}");
            await next();
        });
    }
    
    public static void UseExceptionHandlerMiddleware(this WebApplication app)
    {
        app.Use(async (context, next) =>
        {
            try
            {
                await next();
                if (context.Response.StatusCode == 404)
                {
                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogWarning($"404 Not Found: {context.Request.Method} {context.Request.Path}");
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsJsonAsync(new { message = "The requested resource was not found." });
                }
            }
            catch (Exception ex)
            {
                var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("An error occurred");
            }
        });
    }
}