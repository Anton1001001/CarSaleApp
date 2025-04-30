namespace User.Core.Errors.Base;

public class BadRequestError : BaseError
{
    public BadRequestError(string code, string message) : base(code, message)
    {
    }

    public BadRequestError(string code, string message, List<ValidationError> details) : base(code, message)
    {
        Details = details;
    }
}