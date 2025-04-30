using FluentResults;
using MediatR;

namespace User.Core.CQRS.Commands.ResetPasswordByEmail;

public record ResetPasswordByEmailCommand(string Token, string Email, string NewPassword) : IRequest<Result<ResetPasswordByEmailResponse>>;