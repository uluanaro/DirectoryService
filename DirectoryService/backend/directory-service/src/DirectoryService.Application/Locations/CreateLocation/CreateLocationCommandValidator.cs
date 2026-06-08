namespace DirectoryService.Application.Locations.CreateLocation;

using FluentValidation;

public class CreateLocationCommandValidator : AbstractValidator<CreateLocationCommand>
{
    public CreateLocationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя локации не может быть пустым")
            .MaximumLength(100).WithMessage("Имя локации не может быть длиннее 100 символов");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Адрес не может быть пустым")
            .MaximumLength(250).WithMessage("Адрес не может быть длиннее 250 символов");
    }
}