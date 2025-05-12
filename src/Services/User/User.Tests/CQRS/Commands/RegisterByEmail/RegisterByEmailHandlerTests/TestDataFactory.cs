using User.Core.CQRS.Commands.RegisterByEmail;

namespace User.Tests.CQRS.Commands.RegisterByEmail.RegisterByEmailHandlerTests;

public static class TestDataFactory
{
    public static RegisterByEmailCommand RegisterByEmailCommand =>
        new("test@example.com", "StrongPassword123!", "Alice Test");
}