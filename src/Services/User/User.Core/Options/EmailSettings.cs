namespace User.Core.Options;

public class EmailSettings
{
    public string FromEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmEmailPath { get; set; } = string.Empty;
    public string ResetPasswordPath { get; set; } = string.Empty;
}