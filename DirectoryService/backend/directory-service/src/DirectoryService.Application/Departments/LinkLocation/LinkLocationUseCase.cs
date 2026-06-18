using Directory.Domain.Entities;
using DirectoryService.Application.Departments.Interfaces;
using DirectoryService.Application.Exceptions;
using DirectoryService.Application.Locations.Interfaces;

namespace DirectoryService.Application.Departments.LinkLocation;

public class LinkLocationUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly ILocationRepository _locationRepository;

    public LinkLocationUseCase(
        IDepartmentRepository departmentRepository,
        ILocationRepository locationRepository)
    {
        _departmentRepository = departmentRepository;
        _locationRepository = locationRepository;
    }

    public async Task Handle(LinkLocationCommand command, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetByIdAsync(command.DepartmentId, ct);
        if (department == null)
            throw new DomainException("Департамент не найден.");
       
        var location = await _locationRepository.GetByIdAsync(command.LocationId, ct);
        if (location == null)
            throw new DomainException("Локация не найдена.");

        var linkExists = await _departmentRepository.LinkExistsAsync(command.DepartmentId, command.LocationId, ct);
        if (linkExists) throw new DomainException("Связь уже существует.");

        var link = DepartmentLocation.CreateById(department, command.LocationId);

        await _departmentRepository.AddLocationLinkAsync(link, ct);
    }
}