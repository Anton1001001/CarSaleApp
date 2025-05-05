using User.Core.Errors.Base;

namespace User.Core.Errors;

public class UserNotFoundError(string code = "User.NotFound", string message = "User not found")
    : NotFoundError(code, message);