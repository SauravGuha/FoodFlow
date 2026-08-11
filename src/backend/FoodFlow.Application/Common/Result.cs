
namespace FoodFlow.Application.Common;

public class Result<T>
{
    public bool Status { get; private set; }

    public string Message { get; private set; } = null!;

    public T? Data { get; private set; }

    public object? Value { get; private set; }

    public int StatusCode { get; private set; } = 200;

    public static Result<T> SetSuccess(T data, object? values, int statusCode = 200)
    {
        return new Result<T>
        {
            Status = true,
            Data = data,
            Message = "Operation successful",
            Value = values,
            StatusCode = statusCode
        };
    }

    public static Result<T> SetError(string message, int statusCode = 500)
    {
        return new Result<T>
        {
            Status = false,
            Message = message,
            StatusCode = statusCode
        };
    }

}