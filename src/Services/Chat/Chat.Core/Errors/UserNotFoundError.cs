using Chat.Core.Errors.Base;

namespace Chat.Core.Errors;

public class UserNotFoundError(string code = "User.NotFound", string message = "User not found")
    : NotFoundError(code, message);