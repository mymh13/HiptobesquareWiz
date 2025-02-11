namespace Hiptobesquare;

public record Square(Guid Id, string Colour)
{
    public Guid GetId() => Id;
}