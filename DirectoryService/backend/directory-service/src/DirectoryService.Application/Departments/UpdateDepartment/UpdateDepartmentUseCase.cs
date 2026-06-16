using DirectoryService.Application.Departments.Interfaces;
using DirectoryService.Application.Exceptions;
using FluentValidation;

namespace DirectoryService.Application.Departments.UpdateDepartment;

public class UpdateDepartmentUseCase
{
    private readonly IDepartmentRepository _repository;
    private readonly IValidator<UpdateDepartmentCommand> _validator;

    public UpdateDepartmentUseCase(
        IDepartmentRepository repository,
        IValidator<UpdateDepartmentCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(UpdateDepartmentCommand command, CancellationToken ct = default)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var department = await _repository.GetByIdAsync(command.Id, ct);
        if (department == null)
            throw new DomainException("Департамент не найден.");

        department.UpdateDetails(command.Prefix, command.Name);

        await _repository.UpdateAsync(department, ct);
    }
}    