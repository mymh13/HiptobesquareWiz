using dotenv.net;
using Hiptobesquare.Services;

// Values section: environment variables, create the builder, set the ports, Azure check
DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

var backendUrl = builder.Configuration["BACKEND_URL"] ?? "http://localhost:5173";
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var httpsPort = Environment.GetEnvironmentVariable("API_HTTPS_PORT") ?? "443";
var isAzure = Environment.GetEnvironmentVariable("RUNNING_IN_AZURE") == "true";

// Configure section: Kestrel (reverse proxy) and JSON options
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // PropertyNamingPolicy = Preserve case, so that C# properties match JSON properties: Do not touch!
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(port));

    if (!isAzure)
    {
        options.ListenAnyIP(int.Parse(httpsPort), listenOptions => listenOptions.UseHttps());
    }
});

// Register section: Middleware (logging, rate limiting), application services and CORS-policy
LoggingService.Configure(builder);
RateLimitingService.Configure(builder);

builder.Services.AddSingleton<DataManager>();
builder.Services.AddLogging();
builder.Services.AddSingleton<SquareService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(backendUrl)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Build section: Build the application, use CORS, middleware, API controllers
var app = builder.Build();

app.UseCors("AllowFrontend");

app.UseLoggingMiddleware();
app.UseRateLimiter();
app.UseExceptionHandlerMiddleware();

app.MapControllers();

var logger = app.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("==== API Server Started ====");
logger.LogInformation("Environment: {Environment}", isAzure ? "Azure" : "Local Development");
logger.LogInformation("Listening on: {Urls}", string.Join(", ", app.Urls));
logger.LogInformation("============================");

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

// Run section: Run the application
app.Run();