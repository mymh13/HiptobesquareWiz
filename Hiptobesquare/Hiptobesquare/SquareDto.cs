namespace Hiptobesquare;

public class SquareDto
{
    public string Colour { get; set; } = "";
    public int PositionX { get; set; }  // New
    public int PositionY { get; set; }  // New

    public SquareDto() { } // Required for model binding

    public SquareDto(string colour, int positionX, int positionY)
    {
        Colour = colour;
        PositionX = positionX;
        PositionY = positionY;
    }
}