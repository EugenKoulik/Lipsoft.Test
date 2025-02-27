using System.Text.Json.Serialization;

namespace Lipsoft.BLL.Services.Base;

public class BaseServiceResult
{
    [JsonConstructor]
    private BaseServiceResult() { }

    public bool IsSuccess { get; init; }
    public BaseError? Error { get; init; } 

    private BaseServiceResult(bool isSuccess, BaseError? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static BaseServiceResult Success()
    {
        return new BaseServiceResult(true, null);
    }

    public static BaseServiceResult Failure(BaseError error)
    {
        return new BaseServiceResult(false, error);
    }
}

public class BaseServiceResult<T>
{
    public T Value { get; private set; }
    public bool IsSuccess { get; private set; }
    public BaseError? Error { get; private set; }

    private BaseServiceResult(T value, bool isSuccess, BaseError? error)
    {
        Value = value;
        IsSuccess = isSuccess;
        Error = error;
    }

    public static BaseServiceResult<T> Success(T value)
    {
        return new BaseServiceResult<T>(value, true, null);
    }

    public static BaseServiceResult<T?> Failure(BaseError error)
    {
        return new BaseServiceResult<T?>(default, false, error);
    }
}