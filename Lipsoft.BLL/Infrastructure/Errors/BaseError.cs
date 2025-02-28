namespace Lipsoft.BLL.Infrastructure.Errors;

public abstract class BaseError(string message, Type errorType)
{
    public string Message { get; } = message;
    public Type ErrorType { get; } = errorType ?? throw new ArgumentNullException(nameof(errorType), "Error type cannot be null.");
}