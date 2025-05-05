namespace User.Core.Errors.Base;

public class ValidationError
{
    public string Field { get; set; } = default!;
    public string Message { get; set; } = default!;
}