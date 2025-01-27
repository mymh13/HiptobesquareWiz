namespace Hiptobesquare.Tests;

using Hiptobesquare.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

[TestClass]
public sealed class SquareServiceTests
{
    private SquareService _squareService = null!;
    private MockDataManager _mockDataManager = null!;
    
    [TestInitialize]
    public void Setup()
    {
        _mockDataManager = new MockDataManager();
        _squareService = new SquareService(_mockDataManager);
    }
    
    [TestMethod]
    public async Task AddSquare_ShouldAddSquareToDataManager()
    {
        var squareDto = new SquareDto { Colour = "Red", PositionX = 1, PositionY = 1 };

        await _squareService.AddSquare(squareDto);
        var squares = (await _mockDataManager.ReadSquaresAsync()).ToList();

        Assert.AreEqual(1, squares.Count);
        Assert.AreEqual("Red", squares[0].Colour);
    }

    [TestMethod]
    public async Task GetAllSquares_ShouldReturnAllSquares()
    {
        // Arrange
        var square = new Square { Colour = "Blue", PositionX = 2, PositionY = 2 };
        await _mockDataManager.WriteSquareAsync(square);

        // Act
        var squares = (await _squareService.GetAllSquaresAsync()).ToList();

        // Assert
        Assert.AreEqual(1, squares.Count);
        Assert.AreEqual("Blue", squares[0].Colour);
    }

    [TestMethod]
    public async Task ClearSquares_ShouldRemoveAllSquares()
    {
        // Arrange
        var square = new Square { Colour = "Green", PositionX = 3, PositionY = 3 };
        await _mockDataManager.WriteSquareAsync(square);

        // Act
        await _squareService.ClearSquaresAsync();
        var squares = (await _mockDataManager.ReadSquaresAsync()).ToList();

        // Assert
        Assert.AreEqual(0, squares.Count);
    }

    // Mock for DataManager to avoid file I/O in tests
    private sealed class MockDataManager : DataManager
    {
        private readonly List<Square> _squares = new();

        public override Task<IEnumerable<Square>> ReadSquaresAsync()
        {
            return Task.FromResult<IEnumerable<Square>>(_squares);
        }

        public override Task WriteSquareAsync(Square square)
        {
            _squares.Add(square);
            return Task.CompletedTask;
        }

        public override Task ClearAllFilesAsync()
        {
            _squares.Clear();
            return Task.CompletedTask;
        }
    }
}