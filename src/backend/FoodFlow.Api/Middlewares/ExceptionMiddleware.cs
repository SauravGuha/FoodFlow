
using System.ComponentModel.DataAnnotations;

namespace FoodFlow.Api.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (ValidationException ex)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new { error = string.Join(",\n", ex.ValidationResult.MemberNames) });
        }
        catch (UnauthorizedAccessException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsJsonAsync(new { error = "You are not allowed to access this." });
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = ex.Message });
        }
    }
}