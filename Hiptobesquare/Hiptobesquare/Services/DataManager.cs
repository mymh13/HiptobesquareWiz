namespace Hiptobesquare.Services;

using System.Text.Json;

public class DataManager
{
    private const string DataDirectory = "Data";
    private const string IndexFile = "index.json";
    private const long MaxFileSize = 10 * 1024 * 1024; // 10 Mb
    
    protected DataManager()
    {
        if (!Directory.Exists(DataDirectory))
        {
            Directory.CreateDirectory(DataDirectory);
        }
    }
    
    public virtual async Task<IEnumerable<Square>> ReadSquaresAsync()
    {
        var files = Directory.GetFiles(DataDirectory, "squares_*.json");
        var squares = new List<Square>();
        
        foreach (var file in files)
        {
            var jsonContent = await File.ReadAllTextAsync(file);
            var deserializedSquares = JsonSerializer.Deserialize<List<Square>>(jsonContent) ?? new List<Square>();
            squares.AddRange(deserializedSquares);
        }
        
        return squares;
    }
    
    public virtual async Task WriteSquareAsync(Square square)
    {
        var currentFile = GetOrCreateFile();
        var squares = File.Exists(currentFile) ? JsonSerializer.Deserialize<List<Square>>(await File.ReadAllTextAsync(currentFile)) ?? new List<Square>() : new List<Square>();
    
        squares.Add(square);
        
        await File.WriteAllTextAsync(currentFile, JsonSerializer.Serialize(squares));
    }
    
    public virtual async Task ClearAllFilesAsync()
    {
        var files = Directory.GetFiles(DataDirectory, "squares_*.json");
        
        foreach (var file in files)
        {
            File.Delete(file);
        }
        
        // Reset index
        var indexFile = Path.Combine(DataDirectory, IndexFile);
        await File.WriteAllTextAsync(indexFile, "[]");
    }
    
    internal string GetOrCreateFile()
    {
        var files = Directory.GetFiles(DataDirectory, "squares_*.json").OrderBy(f => f).ToList();
        var currentFile = files.LastOrDefault();
        
        if (currentFile == null || new FileInfo(currentFile).Length > MaxFileSize)
        {
            currentFile = Path.Combine(DataDirectory, $"squares_{Guid.NewGuid()}.json");
            File.Create(currentFile).Close();
        }
        
        return currentFile;
    }
}