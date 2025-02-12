namespace Hiptobesquare.Services;

using System.Text.Json;
using Microsoft.Extensions.Logging;

public class DataManager
{
    // Primary: HOME for Azure, Secondary: .NET adds a bin/Debug/net9.0/ folder to the path
    private static readonly string BaseDirectory = Environment.GetEnvironmentVariable("HOME") 
        ?? Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Data"));
    
    private static readonly string IndexFile = Path.Combine(BaseDirectory, "index.json");
    private const long MaxFileSize = 10 * 1024 * 1024; // 10 MB max file size
    private readonly ILogger<DataManager> _logger;
    
    public DataManager(ILogger<DataManager> logger)
    {
        _logger = logger;

        if (!Directory.Exists(BaseDirectory))
        {
            Directory.CreateDirectory(BaseDirectory);
        }
    }
    
    public virtual async Task<IEnumerable<Square>> ReadSquaresAsync()
    {
        try
        {
            var files = Directory.GetFiles(BaseDirectory, "squares_*.json");
            var squares = new List<Square>();

            foreach (var file in files)
            {
                var jsonContent = await File.ReadAllTextAsync(file);
                if (string.IsNullOrWhiteSpace(jsonContent)) continue;

                var deserialized = JsonSerializer.Deserialize<List<Square>>(jsonContent);
                if (deserialized != null) squares.AddRange(deserialized);
            }
            
            return squares;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex,$"Error reading squares: {ex.Message}");
            return new List<Square>();
        }
    }
    
    public virtual async Task WriteSquareAsync(Square square)
    {
        try
        {
            var filePath = GetOrCreateFile();
            var squares = new List<Square>();

            if (File.Exists(filePath))
            {
                try
                {
                    var json = await File.ReadAllTextAsync(filePath);
                    squares = JsonSerializer.Deserialize<List<Square>>(json) ?? new List<Square>();
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError($"Deserialization error in {filePath}: {jsonEx.Message}");
                }
            }

            squares.Add(square);
            var jsonData = JsonSerializer.Serialize(squares, new JsonSerializerOptions { WriteIndented = true });

            await File.WriteAllTextAsync(filePath, jsonData);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error writing square: {ex.Message}");
        }
    }
    
    public virtual async Task ClearAllFilesAsync()
    {
        try
        {
            foreach (var file in Directory.GetFiles(BaseDirectory, "squares_*.json"))
            {
                File.Delete(file);
            }
            await File.WriteAllTextAsync(Path.Combine(BaseDirectory, IndexFile), "[]");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error clearing files: {ex.Message}");
        }
    }

    private string GetOrCreateFile()
    {
        var files = Directory.GetFiles(BaseDirectory, "squares_*.json").OrderBy(f => f).ToList();
        var currentFile = files.LastOrDefault();

        if (currentFile == null || new FileInfo(currentFile).Length > MaxFileSize)
        {
            int nextFileNumber = files.Count + 1;
            currentFile = Path.Combine(BaseDirectory, $"squares_{nextFileNumber}.json");
            
            File.WriteAllText(currentFile, "[]");
        }

        return currentFile;
    }
}