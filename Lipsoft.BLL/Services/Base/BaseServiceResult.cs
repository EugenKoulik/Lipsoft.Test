using System.Text.Json.Serialization;

namespace Lipsoft.BLL.Services.Base;

public class BaseServiceResult
{
    [JsonConstructor]
    private BaseServiceResult() { }

    public bool IsSuccess { get; init; }
    public string ErrorMessage { get; init; }

    private BaseServiceResult(bool isSuccess, string errorMessage)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static BaseServiceResult Success()
    {
        return new BaseServiceResult(true, string.Empty);
    }

    public static BaseServiceResult Failure(string errorMessage)
    {
        return new BaseServiceResult(false, errorMessage);
    }
}

public class BaseServiceResult<T>
{
    public T Value { get; private set; }
    public bool IsSuccess { get; private set; }
    public string ErrorMessage { get; private set; }
    
    private BaseServiceResult(T value, bool isSuccess, string errorMessage)
    {
        Value = value;
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public static BaseServiceResult<T> Success(T value)
    {
        return new BaseServiceResult<T>(value, true, string.Empty);
    }
    
    public static BaseServiceResult<T?> Failure(string errorMessage)
    {
        return new BaseServiceResult<T?>(default, false, errorMessage);
    }
}