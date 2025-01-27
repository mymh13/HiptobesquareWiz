namespace Hiptobesquare;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

class Program
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddSingleton<DataManager>();
    builder.Services.AddSingleton<SquareService>();

    var app = builder.Build();
    
    static void Main(string[] args)
    {
// GET all /squares
    app.MapGet("/squares", async (SquareService service) =>
    {
        var squares = await service.GetAllSquaresAsync();
        return Results.Ok(squares);
    });

// POST a /squares
app.MapPost("/squares", async (SquareService service, SquareDto squareDto) =>
{
    await service.AddSquare(squareDto);
    return Results.Ok("Square added");
});

// DELETE all /squares
app.MapDelete("/squares", async (SquareService service) =>
{
    await service.ClearSquaresAsync();
    return Results.NoContent();
});

app.Run();
    }
}