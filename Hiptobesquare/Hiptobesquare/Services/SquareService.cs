﻿namespace Hiptobesquare.Services;

public class SquareService
{
    private readonly DataManager _dataManager;

    public SquareService(DataManager dataManager)
    {
        _dataManager = dataManager;
    }
    
    public async Task<IEnumerable<Square>> GetAllSquaresAsync() => await _dataManager.ReadSquaresAsync();

    public async Task AddSquareAsync(SquareDto squareDto)
    {
        var square = new Square(Guid.NewGuid(), squareDto.Colour, squareDto.PositionX, squareDto.PositionY);
        await _dataManager.WriteSquareAsync(square);
    }

    public async Task ClearSquaresAsync() => await _dataManager.ClearAllFilesAsync();
}