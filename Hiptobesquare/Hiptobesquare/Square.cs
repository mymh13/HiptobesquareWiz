namespace Hiptobesquare;

public class Square
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Unique identifier
        public string Colour { get; set; } = string.Empty;
        public int PositionX { get; set; }
        public int PositionY { get; set; }
    }