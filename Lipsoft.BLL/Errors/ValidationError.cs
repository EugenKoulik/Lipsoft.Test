using Lipsoft.BLL.Services.Base;

namespace Lipsoft.BLL.Errors;

public class ValidationError(string message) : BaseError(message, "ValidationError");
