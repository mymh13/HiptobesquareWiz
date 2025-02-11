namespace Hiptobesquare.Services;

using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;

public static class RateLimitingService
{
    public static void Configure(WebApplicationBuilder builder)
    {
        builder.Services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                    factory: _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 40,
                        Window = TimeSpan.FromSeconds(10)
                        // We allow 40 clicks per 10 seconds
                    }
                ));
        });
    }
}