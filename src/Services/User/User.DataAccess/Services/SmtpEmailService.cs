using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using User.Core.Abstractions;
using User.Core.Options;
using User.Core.Services;

namespace User.DataAccess.Services;

public class SmtpEmailService(IOptions<SmtpSettings> smtpSettings, IOptions<EmailSettings> emailSettings)
    : IEmailService
{
    public async Task<bool> SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var smtpSettingsValue = smtpSettings.Value;
        var emailSettingsValue = emailSettings.Value;

        using var smtpClient = new SmtpClient(smtpSettingsValue.SmtpServer);
        smtpClient.Port = smtpSettingsValue.SmtpPort;
        smtpClient.Credentials = new NetworkCredential(emailSettingsValue.FromEmail, emailSettingsValue.Password);
        smtpClient.EnableSsl = true;

        var mailMessage = new MailMessage
        {
            From = new MailAddress(emailSettingsValue.FromEmail),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}