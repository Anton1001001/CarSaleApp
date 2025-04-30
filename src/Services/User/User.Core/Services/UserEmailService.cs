using System.Text;
using Auth.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using User.Core.Abstractions;
using User.Core.Models;
using User.Core.Options;
using static User.Core.Constants.EmailTemplates;

namespace User.Core.Services;

public class UserEmailService(
    UserManager<ApplicationUser> userManager,
    IOptions<JwtSettings> jwtSettings,
    IOptions<EmailSettings> emailSettings,
    IEmailService emailService) : IUserEmailService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    private readonly EmailSettings _emailSettings = emailSettings.Value;
    public async Task<bool> SendConfirmationEmailAsync(ApplicationUser user)
    {
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var url = $"{_jwtSettings.ValidAudience}/{_emailSettings.ConfirmEmailPath}?token={token}&email={user.Email}";

        var htmlMessage = ConfirmationTemplate(url);

        var sendEmailResult = await emailService.SendEmailAsync(user.Email!, "Подтверждение регистрации", htmlMessage);
        
        return sendEmailResult;
    }
    public async Task<bool> SendForgotPasswordEmailAsync(ApplicationUser user)
    {
        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var url = $"{_jwtSettings.ValidAudience}/{_emailSettings.ResetPasswordPath}?token={token}&email={user.Email}";

        var htmlMessage = ForgotPasswordTemplate(user.Name, url);
        
        var sendEmailResult = await emailService.SendEmailAsync(user.Email!, "Сброс пароля", htmlMessage);
        
        return sendEmailResult;

    }
}