using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.API.Extensions;
using User.Core.CQRS.Commands.ConfirmEmail;
using User.Core.CQRS.Commands.ForgotPasswordByEmail;
using User.Core.CQRS.Commands.RegisterByEmail;
using User.Core.CQRS.Commands.ResendEmailConfirmationLink;
using User.Core.CQRS.Commands.ResetPasswordByEmail;
using User.Core.CQRS.Commands.SignInByEmail;
using User.Core.CQRS.Queries.GetCurrentUser;

namespace User.API.Controllers;

[ApiController]
[Route("/api/auth")]
public class AuthController(ISender sender) : ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var response = await sender.Send(new GetCurrentUserQuery(userId), cancellationToken);

        return response.ToActionResult();
    }

    [HttpPost("email/sign-up")]
    public async Task<IActionResult> RegisterByEmail([FromBody] RegisterByEmailCommand request,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);

        return response.ToActionResult();
    }

    [HttpPost("login/sign-in")]
    public async Task<IActionResult> SignInByEmail([FromBody] SignInByEmailCommand request,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);

        return response.ToActionResult();
    }

    [HttpPut("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);

        return response.ToActionResult();
    }

    [HttpPost("resend-email-confirmation-link/{email}")]
    public async Task<IActionResult> ResendEmailConfirmationLink(string email, CancellationToken cancellationToken)
    {
        var response = await sender
            .Send(new ResendEmailConfirmationLinkCommand(email), cancellationToken);

        return response.ToActionResult();
    }

    [HttpPost("forgot-password/{email}")]
    public async Task<IActionResult> ForgotPasswordByEmail(string email, CancellationToken cancellationToken)
    {
        var response = await sender.Send(new ForgotPasswordByEmailCommand(email), cancellationToken);

        return response.ToActionResult();
    }

    [HttpPut("reset-password")]
    public async Task<IActionResult> ResetPasswordByEmail(ResetPasswordByEmailCommand request,
        CancellationToken cancellationToken)
    {
        var response = await sender.Send(request, cancellationToken);

        return response.ToActionResult();
    }
}