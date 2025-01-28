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
        var squareDto = new SquareDto("Red", 1, 1);

        await _squareService.AddSquareAsync(squareDto);
        var squares = (await _mockDataManager.ReadSquaresAsync()).ToList();

        Assert.AreEqual(1, squares.Count);
        Assert.AreEqual("Red", squares[0].Colour);
    }

    [TestMethod]
    public async Task GetAllSquares_ShouldReturnAllSquares()
    {
        // Arrange
        var square = new Square(Guid.NewGuid(), "Blue", 2, 2);
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
        var square = new Square(Guid.NewGuid(), "Green", 3, 3);
        await _mockDataManager.WriteSquareAsync(square);

        // Act
        await _squareService.ClearSquaresAsync();
        var squares = (await _mockDataManager.ReadSquaresAsync()).ToList();

        // Assert
        Assert.AreEqual(0, squares.Count);
    }

    [TestMethod]
    public async Task ShouldCreateNewJsonFile_WhenMaxFileSizeExceeded()
    {
        // Arrange
        _mockDataManager.SimulateFileSizeLimitReached();

        var newSquare = new SquareDto("Yellow", 5, 5);
        await _squareService.AddSquareAsync(newSquare);

        // Act
        var fileCount = _mockDataManager.GetFileCount();

        // Assert
        Assert.AreEqual(2, fileCount, "A new JSON file should be created when size exceeds 10MB");
    }

    // Mock for DataManager to avoid file I/O in tests
    private sealed class MockDataManager : DataManager
    {
        private readonly List<List<Square>> _jsonFiles = new();
        private bool _forceNewFile = false;

        public MockDataManager()
        {
            _jsonFiles.Add(new List<Square>()); // Start with one "file"
        }

        public override Task<IEnumerable<Square>> ReadSquaresAsync()
        {
            var allSquares = _jsonFiles.SelectMany(x => x);
            return Task.FromResult<IEnumerable<Square>>(allSquares);
        }

        public override Task WriteSquareAsync(Square square)
        {
            if (_forceNewFile || _jsonFiles.Last().Count >= 1000) // Simulating 10MB limit
            {
                _jsonFiles.Add(new List<Square>());
                _forceNewFile = false;
            }

            _jsonFiles.Last().Add(square);
            return Task.CompletedTask;
        }

        public override Task ClearAllFilesAsync()
        {
            _jsonFiles.Clear();
            _jsonFiles.Add(new List<Square>()); // Reset to one "file"
            return Task.CompletedTask;
        }

        public void SimulateFileSizeLimitReached() => _forceNewFile = true;
        public int GetFileCount() => _jsonFiles.Count;
    }
}