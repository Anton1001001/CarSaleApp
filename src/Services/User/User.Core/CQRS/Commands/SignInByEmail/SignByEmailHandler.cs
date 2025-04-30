using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using User.Core.Abstractions;
using User.Core.Errors.Base;
using User.Core.Models;
using User.Core.Services;

namespace User.Core.CQRS.Commands.SignInByEmail;

public class SignByEmailHandler(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    IJwtService jwtService,
    IHttpContextAccessor httpContextAccessor
) : IRequestHandler<SignInByEmailCommand, Result<SignInByEmailResponse>>
{
    public async Task<Result<SignInByEmailResponse>> Handle(SignInByEmailCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userManager.FindByNameAsync(request.Email);

        if (user is null)
        {
            return new BadRequestError(code: "Auth.Invalid_SignIn",
                message: "Неверный логин или пароль. Если забыли пароль, восстановите его");
        }
        
        if (!user.EmailConfirmed)
        {
            return new BadRequestError(code: "SignInByEmail.EmailNotConfirmed", message: "Почта не подтверждена");
        }

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return new BadRequestError(code: "Auth.Invalid_SignIn",
                message: "Неверный логин или пароль. Если забыли пароль, восстановите его");
        }

        var accessToken = jwtService.CreateAccessToken(user);

        // var cookieOptions = new CookieOptions
        // {
        //     HttpOnly = true,
        //     Secure = false,
        //     SameSite = SameSiteMode.Strict,
        //     Expires = DateTimeOffset.UtcNow.AddHours(1)
        // };
        //
        // httpContextAccessor.HttpContext?.Response.Cookies.Append("accessToken", accessToken, cookieOptions);

        var response = new SignInByEmailResponse(user.Id, user.Email, user.Name, accessToken);

        return response;
    }
}