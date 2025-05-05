using FluentResults;
using FluentValidation;
using MediatR;
using User.Core.Errors.Base;

namespace User.Core.CQRS.Commands.RegisterByEmail;

public class RegisterByEmailValidationBehavior(IValidator<RegisterByEmailCommand> validator)
    : IPipelineBehavior<RegisterByEmailCommand, Result<RegisterByEmailResponse>>
{
    public async Task<Result<RegisterByEmailResponse>> Handle(RegisterByEmailCommand request,
        RequestHandlerDelegate<Result<RegisterByEmailResponse>> next, CancellationToken cancellationToken)
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