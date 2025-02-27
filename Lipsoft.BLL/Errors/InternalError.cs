using Lipsoft.BLL.Services.Base;

namespace Lipsoft.BLL.Errors;

public class InternalError(string message) : BaseError(message, "InternalError");
