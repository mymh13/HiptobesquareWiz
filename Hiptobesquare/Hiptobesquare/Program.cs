using Hiptobesquare.Services;
using Hiptobesquare;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON settings
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null; // Prevent camelCase conversion
});

// Configure Kestrel server settings
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // HTTP
    options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // HTTPS
});

// Configure logging and rate limiting
LoggingService.Configure(builder);
RateLimitingService.Configure(builder);

// Register application services
builder.Services.AddSingleton<DataManager>();
builder.Services.AddSingleton<SquareService>();

var app = builder.Build();

// Enable CORS (for future frontend integration)
app.UseCors(policy =>
    policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

// Enable necessary middlewares
app.UseLoggingMiddleware();
app.UseRateLimiter();
app.UseExceptionHandlerMiddleware();

// Call Startup.Configure to register API endpoints
var startup = new Startup();
startup.Configure(app);

// Log API startup information
foreach (var endpoint in app.Urls)
{
    Console.WriteLine($"API running on {endpoint}");
    Console.WriteLine("API started...");
}

// Global request logging middleware
app.Use(async (context, next) =>
{
    Console.WriteLine($"Incoming request: {context.Request.Method} {context.Request.Path}");
    await next();
});

// Global exception handling
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"API error: {ex.Message}");
        Console.WriteLine(ex.StackTrace);
        throw; // Propagate the exception
    }
});

app.Run();