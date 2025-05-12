using User.Core.CQRS.Commands.ResetPasswordByEmail;

namespace User.Tests.CQRS.Commands.ResetPasswordByEmail;

public static class TestDataFactory
{
    public static ResetPasswordByEmailCommand ResetPasswordByEmailCommand => 
        new ResetPasswordByEmailCommand(
            Token: "sampleToken",
            Email: "user@example.com",
            NewPassword: "newPassword123"
        );
}

