using FluentResults;
using MediatR;

namespace User.Core.CQRS.Commands.RegisterByEmail;

public record RegisterByEmailCommand(string Email, string Password, string Name)
    : IRequest<Result<RegisterByEmailResponse>>;