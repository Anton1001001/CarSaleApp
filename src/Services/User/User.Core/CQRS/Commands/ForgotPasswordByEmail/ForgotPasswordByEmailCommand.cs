using FluentResults;
using MediatR;

namespace User.Core.CQRS.Commands.ForgotPasswordByEmail;

public record ForgotPasswordByEmailCommand(string Email) : IRequest<Result<ForgotPasswordByEmailResponse>>;