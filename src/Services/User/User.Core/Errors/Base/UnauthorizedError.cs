namespace User.Core.Errors.Base;

public class UnauthorizedError(string code, string message) : BaseError(code, message);