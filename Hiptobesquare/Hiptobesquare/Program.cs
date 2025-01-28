namespace Hiptobesquare;

using Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSingleton<DataManager>();
        builder.Services.AddSingleton<SquareService>();
        builder.Services.AddSingleton<Startup>();

        var startup = builder.Services.BuildServiceProvider().GetRequiredService<Startup>();
        var app = builder.Build();
        startup.Configure(app);
        
        app.Run();
    }
}