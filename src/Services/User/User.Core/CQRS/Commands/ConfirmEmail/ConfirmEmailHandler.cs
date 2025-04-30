using System.Text;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using User.Core.Errors.Base;
using User.Core.Models;

namespace User.Core.CQRS.Commands.ConfirmEmail;

public class ConfirmEmailHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<ConfirmEmailCommand, Result<ConfirmEmailResponse>>
{
    public async Task<Result<ConfirmEmailResponse>> Handle(ConfirmEmailCommand request,
        CancellationToken cancellationToken)
    {
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

        var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

        var result = await userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
        {
            return new BadRequestError(code: "ConfirmEmail.InvalidInput",
                message: "Ошибка при подтверждении почты: токен недействителен или срок его действия истёк.");
        }

        return new ConfirmEmailResponse();
    }
}