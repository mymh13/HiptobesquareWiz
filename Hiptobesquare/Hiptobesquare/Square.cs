namespace Hiptobesquare;

public record Square(Guid Id, string Colour, int PositionX, int PositionY)
{
    public Guid Id { get; init; } = Id;
    public string Colour { get; init; } = Colour;
    public int PositionX { get; init; } = PositionX;
    public int PositionY { get; init; } = PositionY;
}