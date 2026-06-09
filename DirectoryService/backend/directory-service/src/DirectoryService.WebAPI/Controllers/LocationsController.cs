using DirectoryService.Application.Locations.CreateLocation;
using DirectoryService.Contracts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("locations")]
public class LocationsController : ControllerBase
{
    private readonly CreateLocationUseCase _useCase;

    public LocationsController(CreateLocationUseCase useCase)
    {
        _useCase = useCase;
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] CreateLocationRequest request,
        CancellationToken ct)
    {
        var command = new CreateLocationCommand(
            request.Name, 
            $"{request.Address.Street}, {request.Address.City}, {request.Address.Country}");
        var id = await _useCase.Handle(command, ct);
        return Created($"/locations/{id}", new { id });
    }
}
