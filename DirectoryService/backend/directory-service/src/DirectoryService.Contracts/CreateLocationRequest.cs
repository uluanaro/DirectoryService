namespace DirectoryService.Contracts;

public record CreateLocationRequest(
    string Name,
    AddressDto Address
);
