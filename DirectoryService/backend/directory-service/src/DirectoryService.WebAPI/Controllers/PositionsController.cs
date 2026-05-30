using Microsoft.AspNetCore.Mvc;
using DirectoryService.Contracts;

namespace DirectoryService.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PositionsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Array.Empty<PositionResponse>());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreatePositionRequest request)
    {
        var response = new PositionResponse(
            Guid.NewGuid(),
            request.Name,
            request.Description
        );
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] UpdatePositionRequest request)
    {
        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        return NoContent();
    }
}
