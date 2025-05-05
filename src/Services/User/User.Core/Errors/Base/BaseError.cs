using FluentResults;

namespace User.Core.Errors.Base;

public abstract class BaseError(string code, string message) : Error(message)
{
    public string Code { get; set; } = code;
    
    public List<ValidationError>? Details { get; init; }


}