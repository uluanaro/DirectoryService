namespace DirectoryService.Contracts;

public record CreateDepartmentRequest(
    string Prefix,  
    string Name,   
    string Slug,    
    Guid? ParentId 
);
