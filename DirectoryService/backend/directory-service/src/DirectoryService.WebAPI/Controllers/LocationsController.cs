using DirectoryService.Application.Locations.CreateLocation;
using DirectoryService.Application.Locations.UpdateLocation;
using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("locations")]
public class LocationsController : ControllerBase
{
    private readonly CreateLocationUseCase _createUseCase;
    private readonly UpdateLocationUseCase _updateUseCase;

    public LocationsController(CreateLocationUseCase createUseCase,
        UpdateLocationUseCase updateUseCase)
    {
        _createUseCase = createUseCase;
        _updateUseCase = updateUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateLocationRequest request,
        CancellationToken ct)
    {
        var command = new CreateLocationCommand(
            request.Name, 
            $"{request.Address.Street}, {request.Address.City}, {request.Address.Country}");
        var id = await _createUseCase.Handle(command, ct);
        return Created($"/locations/{id}", new { id });
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateLocationRequest request,
        CancellationToken ct)
    {
        var command = new UpdateLocationCommand(id, request.Name, request.Address);
        await _updateUseCase.Handle(command, ct);
        return Ok();
    }
}
