using FluentResults;

namespace File.Core.Errors.Base;

public abstract class BaseError(string code, string message) : Error(message)
{
    public string Code { get; set; } = code;
}