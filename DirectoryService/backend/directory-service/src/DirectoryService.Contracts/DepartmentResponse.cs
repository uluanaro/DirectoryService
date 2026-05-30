namespace DirectoryService.Contracts;

public record DepartmentResponse(
    Guid Id,
    string Prefix,
    string Name,
    string Slug,
    Guid? ParentId
);
