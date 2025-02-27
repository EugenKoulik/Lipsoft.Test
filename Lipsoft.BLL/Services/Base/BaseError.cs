namespace Lipsoft.BLL.Services.Base;

public class BaseError
{
    public string Message { get; }
    public string ErrorType { get; }

    protected BaseError(string message, string errorType)
    {
        Message = message;
        ErrorType = errorType;
    }
}