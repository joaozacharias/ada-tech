using AdaCard.Api.Contracts;

using FluentValidation;

using Newtonsoft.Json;

using Serilog;

using System;
using System.Net;

namespace AdaCard.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch(ValidationException ex)
        {
            await HandleValidationException(context, ex);
        }
        catch (Exception ex)
        {
            Log.Error($"Unexpected error: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private Task HandleValidationException(HttpContext context, ValidationException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var json = new Error
        {
            StatusCode = context.Response.StatusCode,
            Message = "Invalid request",
            Errors = ex.Errors.ToDictionary(x => x.PropertyName, x => x.ErrorMessage)
        };

        return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var json = new Error
        {
            StatusCode = context.Response.StatusCode,
            Message = "Something went wrong!",
            Exception = exception.Message
        };

        return context.Response.WriteAsync(JsonConvert.SerializeObject(json));
    }
}
