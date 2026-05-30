using Microsoft.AspNetCore.Mvc;
using DirectoryService.Infrastructure.Postgres;
using Directory.Domain.Entities;

namespace DirectoryService.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly DirectoryRepository _repository;
    
    public DepartmentsController(DirectoryRepository repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDepartments()
    {
        var departments = await _repository.GetDepartments();
        
        var result = departments.Select(d => new
        {
            id = d.Id,
            namePrefix = d.Name.Prefix,
            name = d.Name.Name,
            slug = d.Slug.Value,
            parentId = d.ParentId
        });
        
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDepartment([FromBody] CreateDepartmentRequest request)
    {
        try
        {
            var departmentName = DepartmentName.Create(request.Prefix, request.Name);
            var slugObj = Slug.Create(request.Slug);
            var department = Department.Create(departmentName, slugObj, request.ParentId);
            
            await _repository.AddDepartment(department);
            
            
            return Ok(new
            {
                id = department.Id,
                namePrefix = department.Name.Prefix,
                name = department.Name.Name,
                slug = department.Slug.Value,
                parentId = department.ParentId
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}

public record CreateDepartmentRequest(
    string Prefix,
    string Name,
    string Slug,
    Guid? ParentId
);
