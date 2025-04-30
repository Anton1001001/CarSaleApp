using FluentResults;
using MediatR;

namespace User.Core.CQRS.Commands.ConfirmEmail;

public record ConfirmEmailCommand(string Token, string Email) : IRequest<Result<ConfirmEmailResponse>>;