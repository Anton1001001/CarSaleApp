using User.Core.CQRS.Commands.ResendEmailConfirmationLink;

namespace User.Tests.CQRS.Commands.ResendEmailConfirmationLink;

public static class TestDataFactory
{
    public static ResendEmailConfirmationLinkCommand ResendEmailConfirmationLinkCommand =>
        new("test@example.com");
}