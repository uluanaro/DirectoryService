using Directory.Domain.Entities;
using DirectoryService.Application.Departments.Interfaces;
using DirectoryService.Application.Exceptions;
using DirectoryService.Application.Locations.Interfaces;
using DirectoryService.Domain.ValueObjects;
using FluentValidation;

namespace DirectoryService.Application.Departments.CreateDepartment;

public class CreateDepartmentUseCase
{
    private readonly IDepartmentRepository _repository;
    private readonly IValidator<CreateDepartmentCommand> _validator;

    public CreateDepartmentUseCase(
        IDepartmentRepository repository,
        IValidator<CreateDepartmentCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateDepartmentCommand command, CancellationToken ct = default)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        
        string? parentPath = null;
        if (command.ParentId.HasValue)
        {
            var parent = await _repository.GetByIdAsync(command.ParentId.Value, ct);
            if (parent == null)
                throw new DomainException("Родительское подразделение не найдено");
            parentPath = parent.Path;
        }

        var allExists = await _repository.AllLocationsExistAsync(command.LocationsId, ct);
        if (allExists == false)
        {
            throw new DomainException("Локаций у даннного департамента пока что не существует");
        }
        
        var department = Department.Create(
            DepartmentName.Create(command.Prefix, command.Name),
            Slug.Create(command.Slug),
            parentPath,
            command.ParentId
            );
        var departmentLocations = command.LocationsId
            .Select(locationId => DepartmentLocation.CreateById(department, locationId))
            .ToList();
        await _repository.AddAsync(department, departmentLocations, ct);

        return department.Id;
    }
}
