// tests/DirectoryService.Domain.Tests/DepartmentTests.cs

using DirectoryService.Domain.Entities;
using Xunit;

public class DepartmentTests
{
    [Fact]
    public void Create_WithEmptyName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() =>
            Department.Create("", "sales"));
    }

    [Fact]
    public void Create_WithValidData_Succeeds()
    {
        var dept = Department.Create("Отдел продаж", "sales");

        Assert.Equal("Отдел продаж", dept.Name.Value);
        Assert.Equal("sales", dept.Slug.Value);
    }
}
