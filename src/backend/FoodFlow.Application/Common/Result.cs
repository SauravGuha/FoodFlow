
namespace FoodFlow.Application.Common;

public class Result<T> where T : class
{
    public bool Status { get; private set; }

    public string Message { get; private set; } = null!;
    public T? Data { get; private set; }

    public object? Values { get; private set; }

    public static Result<T> SetSuccess(T data, object? values)
    {
        return new Result<T>
        {
            Status = true,
            Data = data,
            Message = "Operation successful",
            Values = values
        };
    }

    public static Result<T> SetError(string message)
    {
        return new Result<T>
        {
            Status = false,
            Message = message
        };
    }

}