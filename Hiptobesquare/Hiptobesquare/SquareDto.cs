namespace Hiptobesquare;

public class SquareDto
{
    public string Colour { get; set; } = "";
    public int PositionX { get; set; }
    public int PositionY { get; set; }

    public SquareDto() { } // Constructor without parameters is needed for model binding

    public SquareDto(string colour, int positionX, int positionY)
    {
        Colour = colour;
        PositionX = positionX;
        PositionY = positionY;
    }
}