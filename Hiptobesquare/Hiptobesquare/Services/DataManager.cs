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
            squares.AddRange(JsonSerializer.Deserialize<List<Square>>(jsonContent) ?? new List<Square>());
        }
        return squares;
    }
    
    public virtual async Task WriteSquareAsync(Square square)
    {
        var filePath = GetOrCreateFile();
        var squares = File.Exists(filePath) 
            ? JsonSerializer.Deserialize<List<Square>>(await File.ReadAllTextAsync(filePath)) ?? new List<Square>()
            : new List<Square>();
        
        squares.Add(square);
        await File.WriteAllTextAsync(filePath, JsonSerializer.Serialize(squares));
    }
    
    public virtual async Task ClearAllFilesAsync()
    {
        foreach (var file in Directory.GetFiles(DataDirectory, "squares_*.json"))
        {
            File.Delete(file);
        }
        
        // Reset index
        await File.WriteAllTextAsync(Path.Combine(DataDirectory, IndexFile), "[]");
    }
    
    private string GetOrCreateFile()
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