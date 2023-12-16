using System.Net;
using HR.LeaveManagement.Api.Models;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }


    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            await HandleexceptionAsync(httpContext, e);

        }
    }

    private async Task HandleexceptionAsync(HttpContext httpContext, Exception ex)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem = new();

        switch (ex)
        {
            case BadRequestException badHttpRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails()
                {
                    Title = badHttpRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badHttpRequestException.InnerException?.ToString(),
                    Type = nameof(badHttpRequestException),
                    Errors = badHttpRequestException.ValidationErrors
                };
            break;
            case DirectoryNotFoundException notFoundException:
            break;
            default:
            break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem);

    }
}