namespace Lipsoft.BLL.Infrastructure.Errors;

public class NotFoundError(string message) : BaseError(message, typeof(NotFoundError));