namespace DirectoryService.Contracts;

public record UpdateDepartmentRequest(
    string Prefix,
    string Name,
    string Slug,
    Guid? ParentId
);
