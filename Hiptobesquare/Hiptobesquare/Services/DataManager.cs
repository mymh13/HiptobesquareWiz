namespace Hiptobesquare.Services;

using System.Text.Json;
using Microsoft.Extensions.Logging;

public class DataManager
{
    private const string DataDirectory = "Data";
    private const string IndexFile = "index.json";
    private const long MaxFileSize = 10 * 1024 * 1024; // 10 Mb
    private readonly ILogger<DataManager> _logger;
    
    public DataManager(ILogger<DataManager> logger)
    {
        _logger = logger;
        Directory.CreateDirectory(DataDirectory);
    }
    
    public virtual async Task<IEnumerable<Square>> ReadSquaresAsync()
    {
        try
        {
            var files = Directory.GetFiles(DataDirectory, "squares_*.json");
            var squares = new List<Square>();

            foreach (var file in files)
            {
                var jsonContent = await File.ReadAllTextAsync(file);
                
                if (string.IsNullOrWhiteSpace(jsonContent))
                {
                    _logger.LogWarning($"Skipping empty file: {file}");
                    continue;
                }

                try
                {
                    var deserialized = JsonSerializer.Deserialize<List<Square>>(jsonContent);
                    if (deserialized is not null)
                    {
                        squares.AddRange(deserialized);
                    }
                }
                catch (JsonException jsonEx)
                {
                    _logger.LogError(jsonEx, $"JSON Error deserializing file: {jsonEx.Message}");
                }
            }
            
            return squares;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error reading squares: {ex.Message}");
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
            foreach (var file in Directory.GetFiles(DataDirectory, "squares_*.json"))
            {
                File.Delete(file);
            }
            await File.WriteAllTextAsync(Path.Combine(DataDirectory, IndexFile), "[]");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error clearing files: {ex.Message}");
        }
    }

    private string GetOrCreateFile()
    {
        var files = Directory.GetFiles(DataDirectory, "squares_*.json").OrderBy(f => f).ToList();
        var currentFile = files.LastOrDefault();

        if (currentFile == null || new FileInfo(currentFile).Length > MaxFileSize)
        {
            int nextFileNumber = files.Count + 1;
            currentFile = Path.Combine(DataDirectory, $"squares_{nextFileNumber}.json");
            File.WriteAllText(currentFile, "[]");
        }

        return currentFile;
    }
}