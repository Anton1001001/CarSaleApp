namespace User.Core.Errors.Base;

public class ConflictError(string code, string message) : BaseError(code, message);