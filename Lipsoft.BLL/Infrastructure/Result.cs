using System.Text.Json.Serialization;
using Lipsoft.BLL.Infrastructure.Errors;

namespace Lipsoft.BLL.Infrastructure;

public class Result
{
    [JsonConstructor]
    private Result() { }

    public bool IsSuccess { get; init; }
    public bool IsFailed => !IsSuccess; 
    public BaseError? Error { get; init; }

    private Result(bool isSuccess, BaseError? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success()
    {
        return new Result(true, null);
    }

    public static Result Failure(BaseError error)
    {
        return new Result(false, error);
    }
}

public class Result<T>
{
    private readonly T? _value;

    public bool IsSuccess { get; private set; }
    public bool IsFailed => !IsSuccess;
    public BaseError? Error { get; private set; }

    private Result(T? value, bool isSuccess, BaseError? error)
    {
        _value = value;
        IsSuccess = isSuccess;
        Error = error;
    }
    
    public T? GetValue()
    {
        if (!IsSuccess)
        {
            throw new InvalidOperationException("Cannot access Value because the result is a failure.");
        }
        return _value;
    }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value, true, null);
    }

    public static Result<T> Failure(BaseError error)
    {
        return new Result<T>(default, false, error);
    }
}