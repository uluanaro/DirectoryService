using DirectoryService.Application.Departments.CreateDepartment;
using Microsoft.AspNetCore.Mvc;
using DirectoryService.Contracts;

namespace DirectoryService.WebAPI.Controllers;

[ApiController]
[Route("departments")]
    
    
public class DepartmentsController : ControllerBase
{
    private readonly CreateDepartmentUseCase _useCase;
    
    public DepartmentsController(CreateDepartmentUseCase useCase)
    {
        _useCase = useCase;
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
        
        var id = await _useCase.Handle(command, ct);
        return Created($"/departments/{id}", new { id });
    }
    
}
