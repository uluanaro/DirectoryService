using FluentValidation;
namespace DirectoryService.Application.Departments.CreateDepartment;

public class CreateDepartmentCommandValidator : AbstractValidator<CreateDepartmentCommand>
{
    public CreateDepartmentCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя департамента не может быть пустым")
            .MaximumLength(100).WithMessage("Имя департамента не может быть длиннее 100 символов");
        
        RuleFor(x => x.Slug)
            .NotEmpty().WithMessage("Имя пути не может быть пустым")
            .MaximumLength(100).WithMessage("Имя пути не может быть длиннее 100 символов");

        RuleFor(x => x.LocationsId)
            .NotNull().WithMessage("Список локаций не может не существовать");
    }
}
