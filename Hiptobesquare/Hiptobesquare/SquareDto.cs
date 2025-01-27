﻿namespace Hiptobesquare;

// Data Transfer Object used for incoming data
public class SquareDto
{
    public string Colour { get; set; } = string.Empty;
    public int PositionX { get; set; }
    public int PositionY { get; set; }
}