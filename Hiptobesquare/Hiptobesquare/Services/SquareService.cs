namespace Hiptobesquare.Services;

public class SquareService
{
    private readonly DataManager _dataManager;

    public SquareService(DataManager dataManager)
    {
        _dataManager = dataManager;
    }
    
    public async Task<IEnumerable<Square>> GetAllSquaresAsync()
    {
        return await _dataManager.ReadSquaresAsync();
    }

    public async Task AddSquare(SquareDto squareDto)
    {
        var square = new Square
        {
            Colour = squareDto.Colour,
            PositionX = squareDto.PositionX,
            PositionY = squareDto.PositionY
        };

        await _dataManager.WriteSquareAsync(square);
    }

    public async Task ClearSquaresAsync()
    {
        await _dataManager.ClearAllFilesAsync();
    }
}