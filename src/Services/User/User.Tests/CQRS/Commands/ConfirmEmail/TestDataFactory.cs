using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using User.Core.CQRS.Commands.ConfirmEmail;
using User.Core.Models;

namespace User.Tests.CQRS.Commands.ConfirmEmail;

public static class TestDataFactory
{
    public const string ValidEmail = "test@example.com";
    public const string InvalidEmail = "notfound@example.com";
    public const string AlreadyConfirmedEmail = "confirmed@example.com";
    public const string ValidToken = "valid-token";
    public const string InvalidToken = "invalid-token";

    public static ConfirmEmailCommand CreateValidCommand()
    {
        return new ConfirmEmailCommand(
            Token: Base64Encode(ValidToken),
            Email: ValidEmail
        );
    }

    public static ConfirmEmailCommand CreateInvalidEmailCommand()
    {
        return new ConfirmEmailCommand(
            Token: Base64Encode(ValidToken),
            Email: InvalidEmail
        );
    }

    public static ConfirmEmailCommand CreateAlreadyConfirmedCommand()
    {
        return new ConfirmEmailCommand(
            Token: Base64Encode(ValidToken),
            Email: AlreadyConfirmedEmail
        );
    }

    public static ConfirmEmailCommand CreateInvalidTokenCommand()
    {
        return new ConfirmEmailCommand(
            Token: Base64Encode(InvalidToken),
            Email: ValidEmail
        );
    }

    public static ApplicationUser CreateUser(string email, bool emailConfirmed = false)
    {
        return new ApplicationUser
        {
            Email = email,
            EmailConfirmed = emailConfirmed
        };
    }

    private static string Base64Encode(string token)
    {
        return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
    }
}