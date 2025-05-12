using User.Core.CQRS.Commands.RegisterByEmail;

namespace User.Tests.CQRS.Commands.RegisterByEmail.RegisterByEmailValidationTests;

public static class TestDataFactory
{
    public static RegisterByEmailCommand ValidCommand =>
        new("valid@example.com", "Password123!", "Анна");

    public static RegisterByEmailCommand EmptyEmail =>
        new("", "Password123!", "Анна");

    public static RegisterByEmailCommand ShortEmail =>
        new("a@b", "Password123!", "Анна");

    public static RegisterByEmailCommand InvalidEmailFormat =>
        new("invalid-email", "Password123!", "Анна");

    public static RegisterByEmailCommand LongEmail =>
        new(new string('a', 101) + "@example.com", "Password123!", "Анна");

    public static RegisterByEmailCommand EmptyName =>
        new("valid@example.com", "Password123!", "");

    public static RegisterByEmailCommand InvalidNameCharacters =>
        new("valid@example.com", "Password123!", "Анна123");

    public static RegisterByEmailCommand ShortName =>
        new("valid@example.com", "Password123!", "А");

    public static RegisterByEmailCommand LongName =>
        new("valid@example.com", "Password123!", new string('А', 51));

    public static RegisterByEmailCommand EmptyPassword =>
        new("valid@example.com", "", "Анна");

    public static RegisterByEmailCommand NoUppercasePassword =>
        new("valid@example.com", "password123!", "Анна");

    public static RegisterByEmailCommand NoLowercasePassword =>
        new("valid@example.com", "PASSWORD123!", "Анна");

    public static RegisterByEmailCommand NoDigitPassword =>
        new("valid@example.com", "Password!", "Анна");

    public static RegisterByEmailCommand NoSpecialPassword =>
        new("valid@example.com", "Password123", "Анна");

    public static RegisterByEmailCommand ShortPassword =>
        new("valid@example.com", "P1!a", "Анна");
}