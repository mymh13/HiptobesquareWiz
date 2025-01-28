using Hiptobesquare.Services;
using Hiptobesquare;

var builder = WebApplication.CreateBuilder(args);

// Add Rate Limiting to restrict the number of requests to the API
builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 30,
                Window = TimeSpan.FromSeconds(10)
            }
        ));
});

builder.Services.AddSingleton<DataManager>();
builder.Services.AddSingleton<SquareService>();
builder.Services.AddTransient<Startup>();

var app = builder.Build();

app.UseRateLimiter();

using (var scope = app.Services.CreateScope())
{
    var startup = scope.ServiceProvider.GetRequiredService<Startup>();
    startup.Configure(app);
}

app.Run();