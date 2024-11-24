using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class ExceptionHandler : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception)
        {
            await handleException(context, next);
        }
    }

    public async Task handleException(HttpContext context, RequestDelegate next){
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        ProblemDetails problemDetails = new() {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = "Server error",
            Title = "erver error",
            Detail = "An internal server issue occur"
        };

        await context.Response.WriteAsJsonAsync(problemDetails);
    }
}
