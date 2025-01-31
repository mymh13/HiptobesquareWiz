namespace Hiptobesquare.Services;

public class SquareService(DataManager dataManager)
{
    private readonly DataManager _dataManager = dataManager;

    public async Task<IEnumerable<Square>> GetAllSquaresAsync()
    {
        Console.WriteLine("Fetching all squares.");
    
        try
        {
            var squares = await _dataManager.ReadSquaresAsync();
            Console.WriteLine($"Successfully retrieved {squares.Count()} squares.");
            return squares;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetAllSquaresAsync: {ex.Message}");
            return new List<Square>();
        }
    }
    
    public async Task AddSquareAsync(SquareDto squareDto)
    {
        Console.WriteLine("Adding a new square.");
        var square = new Square(Guid.NewGuid(), squareDto.Colour, squareDto.PositionX, squareDto.PositionY);
    
        Console.WriteLine($"Creating square: {square}");
        await _dataManager.WriteSquareAsync(square);
        Console.WriteLine("Square successfully saved!");
    }

    public async Task ClearSquaresAsync() => await _dataManager.ClearAllFilesAsync();
}