namespace Hiptobesquare.Services;

using Serilog;

public static class LoggingService
{
    public static void Configure(WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders().AddConsole();

        builder.Host.UseSerilog(new LoggerConfiguration()
            .WriteTo.File("Logs/log_connection.log", rollingInterval: RollingInterval.Day)
            .CreateLogger());
    }
}