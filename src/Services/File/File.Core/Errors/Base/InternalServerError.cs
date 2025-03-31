namespace File.Core.Errors.Base;

public class InternalServerError(string code, string message) : BaseError(code, message);