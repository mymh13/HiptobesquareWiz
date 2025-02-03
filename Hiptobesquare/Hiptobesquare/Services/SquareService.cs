namespace Hiptobesquare.Services;

public class SquareService(DataManager dataManager)
{
    private readonly DataManager _dataManager = dataManager;

    public async Task<IEnumerable<Square>> GetAllSquaresAsync()
    {
        try
        {
            return await _dataManager.ReadSquaresAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving squares: {ex.Message}");
            return new List<Square>();
        }
    }
    
    public async Task AddSquareAsync(SquareDto squareDto)
    {
        var square = new Square(Guid.NewGuid(), squareDto.Colour, squareDto.PositionX, squareDto.PositionY);
        await _dataManager.WriteSquareAsync(square);
    }

    public async Task ClearSquaresAsync() => await _dataManager.ClearAllFilesAsync();
}