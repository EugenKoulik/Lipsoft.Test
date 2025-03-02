namespace Lipsoft.BLL.Infrastructure.Errors;

public abstract class BaseError(string message, Type errorType)
{
    public string Message { get; } = message;
}