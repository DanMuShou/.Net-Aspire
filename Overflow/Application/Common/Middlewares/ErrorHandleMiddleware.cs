using System.Text.Json;
using Application.Common.Response;
using Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Middlewares;

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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var result = exception switch
        {
            NotFoundException notFoundException => ServiceResponse<string>.Return404(
                notFoundException.Message
            ),
            CustomValidationException customValidationException =>
                ServiceResponse<string>.Return400(customValidationException.ValidationErrors),
            DataMismatchException dataMismatchException => ServiceResponse<string>.Return400(
                dataMismatchException.Message
            ),
            InvalidOperationException invalidOperationException =>
                ServiceResponse<string>.Return400(invalidOperationException.Message),
            _ => ServiceResponse<string>.Return500(),
        };
        context.Response.StatusCode = result.StatusCode;
        var json = JsonSerializer.Serialize(result);
        return context.Response.WriteAsync(json);
    }
}

public static class ErrorHandleMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandleMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandleMiddleware>();
    }
}
