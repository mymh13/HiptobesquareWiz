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
                        PermitLimit = 25,
                        Window = TimeSpan.FromSeconds(10)
                    }
                ));
        });
    }
}