namespace File.Core.Errors.Base;

public class NotFoundError(string code, string message) : BaseError(code, message);