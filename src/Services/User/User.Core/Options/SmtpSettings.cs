namespace User.Core.Options;

public class SmtpSettings
{
    public string SmtpServer { get; set; } = string.Empty;
    public int SmtpPort { get; set; }
    public string FromEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}