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
        return Ok(departments);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateDepartment(string name, string slug, Guid? parentId = null)
    {
        try
        {
            var departmentName = DepartmentName.Create("", name);
            var slugObj = Slug.Create(slug);
            var department = Department.Create(departmentName, slugObj, parentId);
            
            await _repository.AddDepartment(department);
            return Ok(department);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
