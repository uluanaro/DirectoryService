using System.Data;
using DirectoryService.Application.Departments.CreateDepartment;
using DirectoryService.Application.Departments.Interfaces;
using Microsoft.EntityFrameworkCore;
using DirectoryService.Infrastructure.Postgres;
using DirectoryService.Application.Locations.CreateLocation;
using DirectoryService.Application.Locations.Interfaces;
using DirectoryService.WebAPI.Middleware;
using FluentValidation;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<DirectoryServiceDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILocationRepository, DapperLocationRepository>();

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connection = new NpgsqlConnection(
    builder.Configuration.GetConnectionString("DefaultConnection"));
    connection.Open();
    return connection;
});

builder.Services.AddScoped<CreateLocationUseCase>();
builder.Services.AddScoped<IValidator<CreateLocationCommand>, CreateLocationCommandValidator>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<CreateDepartmentUseCase>();
builder.Services.AddScoped<IValidator<CreateDepartmentCommand>, CreateDepartmentCommandValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();
app.Run();
