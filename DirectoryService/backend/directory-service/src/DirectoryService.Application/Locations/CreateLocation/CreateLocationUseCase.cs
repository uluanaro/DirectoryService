using Directory.Domain.Entities;
using DirectoryService.Application.Exceptions;
using DirectoryService.Application.Locations.Interfaces;
using FluentValidation;

namespace DirectoryService.Application.Locations.CreateLocation;

public class CreateLocationUseCase
{
    private readonly ILocationRepository _repository;
    private readonly IValidator<CreateLocationCommand> _validator;

    public CreateLocationUseCase(
        ILocationRepository repository,
        IValidator<CreateLocationCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task<Guid> Handle(CreateLocationCommand command, CancellationToken ct = default)
    {

        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);


        var nameIsTaken = await _repository.ExistsByNameAsync(command.Name, ct);
        if (nameIsTaken)
            throw new LocationNameAlreadyExistsException(command.Name);


        var location = Location.Create(command.Name, command.Address);


        await _repository.AddAsync(location, ct);

        return location.Id.Value;
    }
}