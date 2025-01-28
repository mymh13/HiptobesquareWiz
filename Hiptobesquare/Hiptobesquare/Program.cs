using Hiptobesquare.Services;
using Hiptobesquare;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
LoggingService.Configure(builder);

// Configure Rate Limiting
RateLimitingService.Configure(builder);

builder.Services.AddSingleton<DataManager>();
builder.Services.AddSingleton<SquareService>();
builder.Services.AddTransient<Startup>();

var app = builder.Build();

// Enable Logging Middleware
app.UseLoggingMiddleware();

// Enable Rate Limiting
app.UseRateLimiter();

// Enable Global Exception Handling Middleware
app.UseExceptionHandlerMiddleware();

using (var scope = app.Services.CreateScope())
{
    var startup = scope.ServiceProvider.GetRequiredService<Startup>();
    startup.Configure(app);
}

app.Run();