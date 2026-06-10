namespace DirectoryService.Application.Exceptions;

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

public class LocationNameAlreadyExistsException : DomainException
{
    public LocationNameAlreadyExistsException(string name)
        : base($"Локация с именем '{name}' уже существует.") { }
}

public class DepartmentNameAlreadyExistsException : DomainException
{
    public DepartmentNameAlreadyExistsException(string name)
        : base($"Департамент с именем '{name}' уже существует.") { }
}