namespace DirectoryService.Contracts;

public record UpdateLocationRequest(
    string Name,
    AddressDto Address
);
