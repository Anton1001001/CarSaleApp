namespace User.Core.Abstractions;

public interface IEmailService
{
    Task<bool> SendEmailAsync(string email, string subject, string htmlMessage);
}