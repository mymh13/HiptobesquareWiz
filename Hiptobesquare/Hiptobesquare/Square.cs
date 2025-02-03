namespace Hiptobesquare;

public record Square(Guid Id, string Colour, int PositionX, int PositionY)
{
    public Guid GetId() => Id;
    public int GetPositionX() => PositionX;
    public int GetPositionY() => PositionY;
}