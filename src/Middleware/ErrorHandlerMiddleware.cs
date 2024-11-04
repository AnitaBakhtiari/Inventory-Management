using InventoryManagement.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace InventoryManagement.Middleware;

public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly JsonSerializerSettings _serializerSettings;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            _logger.LogError(ex, "Business Exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex.ErrorCode, "Business Error", ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected Exception occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, StatusCodes.Status500InternalServerError, "Server Error", "An unexpected error occurred.");
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, int statusCode, string title, string detail)
    {
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var result = JsonConvert.SerializeObject(problemDetails, _serializerSettings);
        await context.Response.WriteAsync(result);
    }
}
