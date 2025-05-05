using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using User.Core.Abstractions;
using User.Core.Errors.Base;
using User.Core.Extensions;
using User.Core.Models;
using User.Core.Services;

namespace User.Core.CQRS.Commands.RegisterByEmail;

public class RegisterByEmailHandler(
    UserManager<ApplicationUser> userManager,
    IUserEmailService userEmailService)
    : IRequestHandler<RegisterByEmailCommand, Result<RegisterByEmailResponse>>
{
    public async Task<Result<RegisterByEmailResponse>> Handle(RegisterByEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Name = request.Name,
            UserName = request.Email,
            Email = request.Email
        };

        var result = await userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            return new BadRequestError(
                code: "RegisterByEmail.InvalidInput",
                message: "Ошибка при создании пользователя");
        }

        var emailConfirmResult = await userEmailService.SendConfirmationEmailAsync(user);

        if (!emailConfirmResult)
        {
            return new BadRequestError(
                code: "RegisterByEmail.EmailFailed",
                message: $"Не удалось отправить письмо на {request.Email}"
            );
        }

        return new RegisterByEmailResponse();
    }
}