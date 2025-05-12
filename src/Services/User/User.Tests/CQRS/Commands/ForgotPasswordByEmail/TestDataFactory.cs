using User.Core.CQRS.Commands.ForgotPasswordByEmail;
using User.Core.Models;

namespace User.Tests.CQRS.Commands.ForgotPasswordByEmail;

public static class TestDataFactory
{
    public const string ValidEmail = "valid@example.com";
    public const string InvalidEmail = "invalid@example.com";
    public const string UnconfirmedEmail = "unconfirmed@example.com";

    public static ForgotPasswordByEmailCommand CreateValidCommand()
    {
        return new ForgotPasswordByEmailCommand(ValidEmail);
    }

    public static ForgotPasswordByEmailCommand CreateInvalidEmailCommand()
    {
        return new ForgotPasswordByEmailCommand(InvalidEmail);
    }

    public static ForgotPasswordByEmailCommand CreateEmptyEmailCommand()
    {
        return new ForgotPasswordByEmailCommand(string.Empty);
    }

    public static ForgotPasswordByEmailCommand CreateUnconfirmedEmailCommand()
    {
        return new ForgotPasswordByEmailCommand(UnconfirmedEmail);
    }

    public static ApplicationUser CreateUser(string email, bool emailConfirmed = true)
    {
        return new ApplicationUser
        {
            Email = email,
            EmailConfirmed = emailConfirmed
        };
    }
}