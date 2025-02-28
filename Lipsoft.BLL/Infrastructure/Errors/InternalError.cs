namespace Lipsoft.BLL.Infrastructure.Errors;

public class InternalError(string message) : BaseError(message, typeof(InternalError));
