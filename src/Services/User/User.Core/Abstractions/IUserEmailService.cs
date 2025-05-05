using User.Core.Models;

namespace User.Core.Abstractions;

public interface IUserEmailService
{
    Task<bool> SendConfirmationEmailAsync(ApplicationUser user);
    Task<bool> SendForgotPasswordEmailAsync(ApplicationUser user);
}