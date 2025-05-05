using FluentResults;
using FluentValidation;
using MediatR;
using User.Core.Errors.Base;

namespace User.Core.CQRS.Commands.SignInByEmail;

public class SignInByEmailValidationBehavior(IValidator<SignInByEmailCommand> validator)
    : IPipelineBehavior<SignInByEmailCommand, Result<SignInByEmailResponse>>
{
    public async Task<Result<SignInByEmailResponse>> Handle(SignInByEmailCommand request,
        RequestHandlerDelegate<Result<SignInByEmailResponse>> next, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors;

            var groupedErrors = errors
                .GroupBy(e => e.PropertyName)
                .Select(g => g.First())
                .ToList();

            var errorMessages = groupedErrors
                .Select(error => new ValidationError { Field = error.PropertyName, Message = error.ErrorMessage })
                .ToList();

            return new BadRequestError(
                code: "ValidationFailed",
                message: "Некорректные данные",
                details: errorMessages
            );
        }

        return await next(cancellationToken);
    }
}