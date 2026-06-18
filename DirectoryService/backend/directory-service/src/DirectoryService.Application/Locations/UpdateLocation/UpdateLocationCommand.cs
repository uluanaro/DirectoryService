namespace DirectoryService.Application.Locations.UpdateLocation;

public record UpdateLocationCommand(
    Guid Id,
    string Name,
    string Address
);
