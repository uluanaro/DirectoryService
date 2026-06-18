using DirectoryService.Application.Exceptions;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace DirectoryService.WebAPI.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException ex)
        {
            await WriteResponse(context, HttpStatusCode.BadRequest,
                ex.Errors.Select(e => e.ErrorMessage));
        }
        catch (DomainException ex)
        {
            await WriteResponse(context, HttpStatusCode.BadRequest,
                new[] { ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Необработанное исключение");
            await WriteResponse(context, HttpStatusCode.InternalServerError,
                new[] { "Внутренняя ошибка сервера" });
        }
    }

    private static async Task WriteResponse(
        HttpContext context,
        HttpStatusCode statusCode,
        IEnumerable<string> errors)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var response = new { errors };
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}