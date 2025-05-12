using User.Core.CQRS.Commands.SignInByEmail;
using User.Core.Models;

namespace User.Tests.CQRS.Commands.SignInByEmail;

public static class TestDataFactory
{
    public static SignInByEmailCommand SignInByEmailCommand => new SignInByEmailCommand(
        Email: "testuser@example.com", 
        Password: "TestPassword123"
    );

    public static ApplicationUser GetTestUser(string email, bool emailConfirmed = true) => new ApplicationUser
    {
        Id = "userId",
        Email = email,
        Name = "John Doe",
        EmailConfirmed = emailConfirmed
    };
}
