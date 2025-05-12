using FluentValidation.TestHelper;
using User.Core.CQRS.Commands.RegisterByEmail;

namespace User.Tests.CQRS.Commands.RegisterByEmail.RegisterByEmailValidationTests;

public class RegisterByEmailCommandValidatorTests
{
    private readonly RegisterByEmailCommandValidator _validator = new();

    [Fact]
    public void Valid_Command_Should_Pass()
    {
        var result = _validator.TestValidate(TestDataFactory.ValidCommand);
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Empty_Email_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.EmptyEmail).ShouldHaveValidationErrorFor(x => x.Email);

    [Fact]
    public void Short_Email_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.ShortEmail).ShouldHaveValidationErrorFor(x => x.Email);

    [Fact]
    public void Invalid_Email_Format_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.InvalidEmailFormat).ShouldHaveValidationErrorFor(x => x.Email);

    [Fact]
    public void Long_Email_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.LongEmail).ShouldHaveValidationErrorFor(x => x.Email);

    [Fact]
    public void Empty_Name_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.EmptyName).ShouldHaveValidationErrorFor(x => x.Name);

    [Fact]
    public void Invalid_Name_Characters_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.InvalidNameCharacters).ShouldHaveValidationErrorFor(x => x.Name);

    [Fact]
    public void Short_Name_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.ShortName).ShouldHaveValidationErrorFor(x => x.Name);

    [Fact]
    public void Long_Name_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.LongName).ShouldHaveValidationErrorFor(x => x.Name);

    [Fact]
    public void Empty_Password_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.EmptyPassword).ShouldHaveValidationErrorFor(x => x.Password);

    [Fact]
    public void Password_Without_Uppercase_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.NoUppercasePassword).ShouldHaveValidationErrorFor(x => x.Password);

    [Fact]
    public void Password_Without_Lowercase_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.NoLowercasePassword).ShouldHaveValidationErrorFor(x => x.Password);

    [Fact]
    public void Password_Without_Digit_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.NoDigitPassword).ShouldHaveValidationErrorFor(x => x.Password);

    [Fact]
    public void Password_Without_Special_Character_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.NoSpecialPassword).ShouldHaveValidationErrorFor(x => x.Password);

    [Fact]
    public void Short_Password_Should_Fail() =>
        _validator.TestValidate(TestDataFactory.ShortPassword).ShouldHaveValidationErrorFor(x => x.Password);
}