namespace Hiptobesquare.Controllers;

using Microsoft.AspNetCore.Mvc;
using Services;

[ApiController]
[Route("squares")]
public class SquareController(SquareService squareService) : ControllerBase
{
    private readonly SquareService _squareService = squareService;
    
    [HttpGet]
    public async Task<IActionResult> GetSquares()
    {
        var squares = await _squareService.GetAllSquaresAsync();
        return Ok(squares);
    }

    [HttpPost]
    public async Task<IActionResult> AddSquare([FromBody] SquareDto squareDto)
    {
        await _squareService.AddSquareAsync(squareDto);
        return Ok("Square added");
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteSquares()
    {
        await _squareService.ClearSquaresAsync();
        return NoContent();
    }
}