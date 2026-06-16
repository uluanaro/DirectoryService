namespace DirectoryService.Application.Departments.UpdateDepartment;

public record UpdateDepartmentCommand(
    Guid Id,
    string Prefix,
    string Name
);
