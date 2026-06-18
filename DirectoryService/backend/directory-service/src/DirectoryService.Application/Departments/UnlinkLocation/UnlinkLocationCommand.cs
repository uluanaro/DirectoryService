namespace DirectoryService.Application.Departments.UnlinkLocation;

public record UnlinkLocationCommand
    ( 
        Guid DepartmentId,
        Guid LocationId
    );