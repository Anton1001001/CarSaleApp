using System.Text;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using User.Core.Errors.Base;
using User.Core.Extensions;
using User.Core.Models;

namespace User.Core.CQRS.Commands.ResetPasswordByEmail;

public class ResetPasswordByEmailHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<ResetPasswordByEmailCommand, Result<ResetPasswordByEmailResponse>>
{
    public async Task<Result<ResetPasswordByEmailResponse>> Handle(ResetPasswordByEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            return new BadRequestError(code: "ConfirmEmail.InvalidInput",
                message: $"Пользователь email {request.Email} не найден.");
        }
        
        var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
        
        var result = await userManager.ResetPasswordAsync(user, token, request.NewPassword);

        if (!result.Succeeded)
        {
            return new BadRequestError(code: "ResetPasswordError", message: "Ошибка при сбросе пароля");
        }

        return new ResetPasswordByEmailResponse();
    }
}