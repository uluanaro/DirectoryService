using FluentValidation;
namespace DirectoryService.Application.Locations.UpdateLocation;

// public record UpdateLocationCommand(
    // Guid Id,
    // string Name,
    // string Address
// );

public class UpdateLocationCommandValidator : AbstractValidator<UpdateLocationCommand>
{
    public UpdateLocationCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Имя не может быть пустым")
            .MaximumLength(100).WithMessage("Имя слишком длинное");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Адрес не может быть пустым");
    }
}



