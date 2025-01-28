using Hiptobesquare.Services;
using Hiptobesquare;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<DataManager>();
builder.Services.AddSingleton<SquareService>();
builder.Services.AddTransient<Startup>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var startup = scope.ServiceProvider.GetRequiredService<Startup>();
    startup.Configure(app);
}

app.Run();