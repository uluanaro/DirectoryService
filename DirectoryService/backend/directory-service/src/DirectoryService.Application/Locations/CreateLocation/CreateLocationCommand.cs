using DirectoryService.Contracts;

namespace DirectoryService.Application.Locations.CreateLocation;

public record CreateLocationCommand(string Name, string Address);