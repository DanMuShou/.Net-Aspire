using System.Net;
using System.Text.Json;
using Application.Exceptions;

namespace PostServer.Middlewares;

public class ErrorHandleMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var httpStatusCode = HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var result = string.Empty;

        switch (exception)
        {
            case NotFoundException notFoundException:
                httpStatusCode = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(notFoundException.Message);
                break;
            case CustomValidationException customValidationException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(customValidationException.ValidationErrors);
                break;
            case DataMismatchException dataMismatchException:
                httpStatusCode = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(dataMismatchException.Message);
                break;
        }

        context.Response.StatusCode = (int)httpStatusCode;
        return context.Response.WriteAsync(result);
    }
}

public static class ErrorHandleMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandleMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandleMiddleware>();
    }
}
