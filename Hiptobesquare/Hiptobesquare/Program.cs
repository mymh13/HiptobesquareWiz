using Hiptobesquare.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure JSON serialization settings
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
builder.Services.AddLogging();
builder.Services.AddSingleton<SquareService>();

var app = builder.Build();

// Enable CORS (for frontend integration)
app.UseCors(policy =>
    policy.WithOrigins("http://localhost:5173") // Eller den port din frontend kör på
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