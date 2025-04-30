using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using User.Core.Abstractions;
using User.Core.Errors.Base;
using User.Core.Models;
using User.Core.Services;

namespace User.Core.CQRS.Commands.ResendEmailConfirmationLink;

public class ResendEmailConfirmationLinkHandler(
    UserManager<ApplicationUser> userManager,
    IUserEmailService userEmailService) : IRequestHandler<ResendEmailConfirmationLinkCommand,
    Result<ResendEmailConfirmationLinkResponse>>
{
    public async Task<Result<ResendEmailConfirmationLinkResponse>> Handle(ResendEmailConfirmationLinkCommand request,
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

        if (user.EmailConfirmed)
        {
            return new BadRequestError(code: "ConfirmEmail.InvalidInput",
                message: $"Почта {request.Email} уже подтверждена");
        }

        var emailConfirmResult = await userEmailService.SendConfirmationEmailAsync(user);

        if (!emailConfirmResult)
        {
            return new BadRequestError(
                code: "RegisterByEmail.EmailFailed",
                message: $"Не удалось отправить письмо на {request.Email}"
            );       
        }

        return new ResendEmailConfirmationLinkResponse();
    }
}