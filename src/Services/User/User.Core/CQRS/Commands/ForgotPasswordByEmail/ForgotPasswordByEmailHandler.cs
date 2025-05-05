using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using User.Core.Abstractions;
using User.Core.Errors.Base;
using User.Core.Models;
using User.Core.Services;

namespace User.Core.CQRS.Commands.ForgotPasswordByEmail;

public class ForgotPasswordByEmailHandler(UserManager<ApplicationUser> userManager, IUserEmailService userEmailService)
    : IRequestHandler<ForgotPasswordByEmailCommand, Result<ForgotPasswordByEmailResponse>>
{
    public async Task<Result<ForgotPasswordByEmailResponse>> Handle(ForgotPasswordByEmailCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Email))
        {
            return new BadRequestError(code: "ResendEmailConfirmationLink.InvalidInput", message: "Invalid email");
        }

        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return new BadRequestError(code: "ConfirmEmail.InvalidInput",
                message: $"Пользователь email {request.Email} не найден.");
        }
        
        if (!user.EmailConfirmed)
        {
            return new BadRequestError(code: "ForgotPasswordError", message: "Email не подтвержден.");
        }


        var emailConfirmResult = await userEmailService.SendForgotPasswordEmailAsync(user);

        if (!emailConfirmResult)
        {
            return new BadRequestError(
                code: "ForgotPasswordByEmail.EmailFailed",
                message: $"Не удалось отправить письмо на {request.Email}"
            );
        }

        return new ForgotPasswordByEmailResponse();
    }
}