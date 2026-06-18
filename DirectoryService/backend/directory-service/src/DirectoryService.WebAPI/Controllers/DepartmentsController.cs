using DirectoryService.Application.Departments.CreateDepartment;
using DirectoryService.Application.Departments.LinkLocation;
using DirectoryService.Application.Departments.UnlinkLocation;
using DirectoryService.Application.Departments.UpdateDepartment;
using Microsoft.AspNetCore.Mvc;
using DirectoryService.Contracts;

namespace DirectoryService.WebAPI.Controllers;

[ApiController]
[Route("departments")]
    
    
public class DepartmentsController : ControllerBase
{
    private readonly CreateDepartmentUseCase _createUseCase;
    private readonly UpdateDepartmentUseCase _updateUseCase;
    private readonly LinkLocationUseCase _linkUseCase;
    private readonly UnlinkLocationUseCase _unlinkUseCase;
    
    
    public DepartmentsController(CreateDepartmentUseCase createUseCase,
        UpdateDepartmentUseCase updateUseCase,
        LinkLocationUseCase linkUseCase,
        UnlinkLocationUseCase unlinkUseCase)
    {
        _createUseCase = createUseCase;
        _updateUseCase = updateUseCase;
        _linkUseCase = linkUseCase;
        _unlinkUseCase = unlinkUseCase;
    
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateDepartmentRequest request,
        CancellationToken ct)
    {
        var command = new CreateDepartmentCommand(
            request.Prefix,
            request.Name,
            request.Slug,
            request.ParentId,
            request.LocationsId);
        
        var id = await _createUseCase.Handle(command, ct);
        return Created($"/departments/{id}", new { id });
    }
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id, [FromBody] UpdateDepartmentRequest request, CancellationToken ct)
    {
        var command = new UpdateDepartmentCommand(id, request.Prefix, request.Name);
        await _updateUseCase.Handle(command, ct);
        return Ok();
    }

    [HttpPost("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> LinkLocation(
        Guid departmentId, Guid locationId, CancellationToken ct)
    {
        var command = new LinkLocationCommand(departmentId, locationId);
        await _linkUseCase.Handle(command, ct);
        return Ok();
    }

    [HttpDelete("{departmentId:guid}/locations/{locationId:guid}")]
    public async Task<IActionResult> UnlinkLocation(
        Guid departmentId, Guid locationId, CancellationToken ct)
    {
        var command = new UnlinkLocationCommand(departmentId, locationId);
        await _unlinkUseCase.Handle(command, ct);
        return Ok();
    }
}
