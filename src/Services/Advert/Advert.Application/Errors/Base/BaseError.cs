using FluentResults;

namespace Advert.Application.Errors.Base;

public abstract class BaseError(string code, string message) : Error(message)
{
    public string Code { get; set; } = code;
}