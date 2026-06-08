using Microsoft.AspNetCore.Mvc;
using DirectoryService.Contracts;

namespace DirectoryService.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(Array.Empty<DepartmentResponse>());
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetById(Guid id)
    {
        return NotFound();
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateDepartmentRequest request)
    {
        var response = new DepartmentResponse(
            Guid.NewGuid(),
            request.Prefix,
            request.Name,
            request.Slug,
            request.ParentId
        );
        return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] UpdateDepartmentRequest request)
    {
        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        return NoContent();
    }
}
