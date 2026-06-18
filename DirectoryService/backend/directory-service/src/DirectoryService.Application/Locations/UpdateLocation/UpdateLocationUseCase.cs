using Directory.Domain.Entities;
using DirectoryService.Application.Exceptions;
using DirectoryService.Application.Locations.CreateLocation;
using DirectoryService.Application.Locations.Interfaces;
using FluentValidation;

namespace DirectoryService.Application.Locations.UpdateLocation;

public class UpdateLocationUseCase
{
    private readonly ILocationRepository _repository;
    private readonly IValidator<UpdateLocationCommand> _validator;

    public UpdateLocationUseCase(
        ILocationRepository repository,
        IValidator<UpdateLocationCommand> validator)
    {
        _repository = repository;
        _validator = validator;
    }

    public async Task Handle(UpdateLocationCommand command, CancellationToken ct = default)
    {
        var validationResult = await _validator.ValidateAsync(command, ct);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var location = await _repository.GetByIdAsync(command.Id, ct);
        if (location == null)
            throw new DomainException("Локация не найдена.");
        
        location.UpdateDetails(command.Name, command.Address);

        await _repository.UpdateAsync(location, ct);

    }
}