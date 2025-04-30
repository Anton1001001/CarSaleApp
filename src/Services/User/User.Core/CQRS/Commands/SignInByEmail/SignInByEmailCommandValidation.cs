using FluentValidation;

namespace User.Core.CQRS.Commands.SignInByEmail;

public class SignInByEmailCommandValidation : AbstractValidator<SignInByEmailCommand>
{
    public SignInByEmailCommandValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email обязателен.")
            .MinimumLength(5).WithMessage("Email должен содержать не менее 5 символов.")
            .MaximumLength(100).WithMessage("Email не должен превышать 100 символов.")
            .EmailAddress().WithMessage("Некорректный формат email.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен.")
            .MinimumLength(8).WithMessage("Пароль должен содержать не менее 8 символов.")
            .MaximumLength(100).WithMessage("Пароль не должен превышать 100 символов.")
            .Matches("[A-Z]").WithMessage("Пароль должен содержать хотя бы одну заглавную букву.")
            .Matches("[a-z]").WithMessage("Пароль должен содержать хотя бы одну строчную букву.")
            .Matches("[0-9]").WithMessage("Пароль должен содержать хотя бы одну цифру.")
            .Matches("[^a-zA-Z0-9]").WithMessage("Пароль должен содержать хотя бы один специальный символ.");
    }
}