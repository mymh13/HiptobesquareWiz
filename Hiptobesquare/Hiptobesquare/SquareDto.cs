namespace Hiptobesquare;

public class SquareDto
{
    public string Colour { get; set; } = "";

    public SquareDto() { } // Constructor without parameters is needed for model binding

    public SquareDto(string colour)
    {
        Colour = colour;
    }
}