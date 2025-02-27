using Lipsoft.BLL.Services.Base;

namespace Lipsoft.BLL.Errors;

public class NotFoundError(string message) : BaseError(message, "NotFound");