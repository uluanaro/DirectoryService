using DirectoryService.Application.Locations.UpdateLocation;
using FluentValidation;

namespace DirectoryService.Application.Departments.UpdateDepartment;

public class UpdateDepartmentCommandValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentCommandValidator()
    {
        RuleFor(x => x.Prefix)
            .NotEmpty().WithMessage("Префикс не может быть пустым")
            .MaximumLength(50).WithMessage("Префикс слишком длинный");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя не может быть пустым")
            .MaximumLength(50).WithMessage("Имя слишком длинное");
    }
}
