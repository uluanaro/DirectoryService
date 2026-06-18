namespace DirectoryService.Application.Departments.LinkLocation;

public record LinkLocationCommand
( 
    Guid DepartmentId,
    Guid LocationId
 );
    