namespace Hiptobesquare;

public class SquareDto
{
    public string Colour { get; set; } = ""; // Default to an empty string to avoid null ref exceptions
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public SquareDto() { } // Required for model binding

    public SquareDto(string colour, int positionX, int positionY) =>
        (Colour, PositionX, PositionY) = (colour, positionX, positionY);
}