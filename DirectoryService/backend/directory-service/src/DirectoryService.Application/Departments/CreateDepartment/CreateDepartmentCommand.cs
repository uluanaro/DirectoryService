using Directory.Domain.Entities;

namespace DirectoryService.Application.Departments.CreateDepartment;

public record CreateDepartmentCommand(
    string Prefix,
    string Name, 
    string Slug, 
    Guid? ParentId, 
    List<Guid> LocationsId);