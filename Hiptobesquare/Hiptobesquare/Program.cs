using dotenv.net;
using Hiptobesquare.Services;

// Load environment variables from .env file
DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

// Configure JSON serialization settings
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

// Load Environment Variables
var backendUrl = builder.Configuration["BACKEND_URL"] ?? "http://localhost:5173";
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080"; // Default HTTP Port
var httpsPort = Environment.GetEnvironmentVariable("HTTPS_PORT") ?? "8443"; // Default HTTPS Port

// Determine if running in Azure
bool isAzure = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));

// Configure Kestrel dynamically based on environment
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port)); // HTTP

    if (!isAzure) // Only enable HTTPS for local development
    {
        options.ListenAnyIP(int.Parse(httpsPort), listenOptions => listenOptions.UseHttps());
    }
});

// Configure logging and rate limiting
LoggingService.Configure(builder);
RateLimitingService.Configure(builder);

// Register application services
builder.Services.AddSingleton<DataManager>();
builder.Services.AddLogging();
builder.Services.AddSingleton<SquareService>();

var app = builder.Build();

// Enable CORS (for frontend integration)
app.UseCors(policy =>
    policy.WithOrigins(backendUrl)
        .AllowAnyMethod()
        .AllowAnyHeader());

// Enable necessary middlewares
app.UseLoggingMiddleware();
app.UseRateLimiter();
app.UseExceptionHandlerMiddleware();

// Register API controllers
app.MapControllers();

// Log API startup information
foreach (var endpoint in app.Urls)
{
    Console.WriteLine($"API running on {endpoint}");
}

// Global middleware for request logging and exception handling
app.Use(async (context, next) =>
{
    try
    {
        Console.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");
        await next();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"API error: {ex.Message}\n{ex.StackTrace}");
        throw;
    }
});

app.Run();