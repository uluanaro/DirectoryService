namespace DirectoryService.Contracts;

public record PositionResponse(
    Guid Id,
    string Name,
    string Description
);
