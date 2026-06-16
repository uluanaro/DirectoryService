using Directory.Domain.Entities;
using DirectoryService.Application.Departments.Interfaces;
using DirectoryService.Application.Departments.LinkLocation;
using DirectoryService.Application.Exceptions;
using DirectoryService.Application.Locations.Interfaces;

namespace DirectoryService.Application.Departments.UnlinkLocation;

public class UnlinkLocationUseCase
{
    private readonly IDepartmentRepository _departmentRepository;
    
    public UnlinkLocationUseCase(
        IDepartmentRepository departmentRepository,
        ILocationRepository locationRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task Handle(UnlinkLocationCommand command, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetByIdAsync(command.DepartmentId, ct);
        if (department == null)
            throw new DomainException("Департамент не найден.");

        var linkExists = await _departmentRepository.LinkExistsAsync(command.DepartmentId, command.LocationId, ct);
        if (!linkExists)
        {
            throw new DomainException("Связь не найдена.");
        }
        

        await _departmentRepository.RemoveLocationLinkAsync(command.DepartmentId, command.LocationId, ct);
    }
}