using Microsoft.AspNetCore.Mvc;
using DirectoryService.Contracts;

namespace DirectoryService.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Array.Empty<LocationResponse>());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateLocationRequest request)
    {
        var response = new LocationResponse(
            Guid.NewGuid(),
            request.Name,
            request.Address
        );
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] UpdateLocationRequest request)
    {
        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        return NoContent();
    }
}
