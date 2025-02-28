namespace Lipsoft.BLL.Infrastructure.Errors;

public class ValidationError(string message) : BaseError(message, typeof(ValidationError));
