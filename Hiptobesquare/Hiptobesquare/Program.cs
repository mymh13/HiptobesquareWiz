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
var port = builder.Configuration["API_PORT"] ?? "5000";
var httpsPort = builder.Configuration["API_HTTPS_PORT"] ?? "5001";

// Configure Kestrel dynamically based on environment
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port)); // HTTP
    options.ListenAnyIP(int.Parse(httpsPort), listenOptions => listenOptions.UseHttps()); // HTTPS
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