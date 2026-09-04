
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace FoodFlow.Api.Middlewares;

/// <summary>
/// Middleware that handles exceptions and returns appropriate responses.
/// </summary>
public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (DBConcurrencyException)
        {
            context.Response.StatusCode = 409;
            await context.Response.WriteAsJsonAsync(new { error = "The record has been modified by another user." });
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