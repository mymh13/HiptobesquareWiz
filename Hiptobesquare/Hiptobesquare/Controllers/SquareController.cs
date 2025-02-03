namespace Hiptobesquare;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services;

public class Startup
{
    public void Configure(WebApplication app)
    {
        // TODO: Add logging for testing purposes
        var logger = app.Services.GetRequiredService<ILogger<Startup>>();
        logger.LogInformation("🚀 Startup.Configure() körs!");
        
        // app.MapGet("/squares", async (SquareService service) => Results.Ok(await service.GetAllSquaresAsync()));
        
        app.MapGet("/squares", async (SquareService service) =>
        {
            logger.LogInformation("GET /squares called");
            var squares = await service.GetAllSquaresAsync();
            return Results.Ok(squares);
        });

        app.MapPost("/squares", async (SquareService service, SquareDto squareDto) =>
        {
            logger.LogInformation("POST /squares called"); // Add logging for testing purposes
            await service.AddSquareAsync(squareDto);
            return Results.Ok("Square added");
        });

        app.MapDelete("/squares", async (SquareService service) =>
        {
            logger.LogInformation("DELETE /squares called"); // Add logging for testing purposes
            await service.ClearSquaresAsync();
            return Results.NoContent();
        });
    }
}