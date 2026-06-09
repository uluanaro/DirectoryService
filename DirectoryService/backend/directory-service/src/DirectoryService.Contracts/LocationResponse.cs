namespace DirectoryService.Contracts;

public record LocationResponse(
    Guid Id,
    string Name,
    AddressDto Address
);
