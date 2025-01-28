namespace Hiptobesquare;

using Services;

public class Startup
{
    public void Configure(WebApplication app)
    {
        app.MapGet("/squares", async (SquareService service) => Results.Ok(await service.GetAllSquaresAsync()));

        app.MapPost("/squares", async (SquareService service, SquareDto squareDto) =>
        {
            await service.AddSquareAsync(squareDto);
            return Results.Ok("Square added");
        });

        app.MapDelete("/squares", async (SquareService service) =>
        {
            await service.ClearSquaresAsync();
            return Results.NoContent();
        });
    }
}