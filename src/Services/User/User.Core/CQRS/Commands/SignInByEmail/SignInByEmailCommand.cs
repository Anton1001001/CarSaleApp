using FluentResults;
using MediatR;

namespace User.Core.CQRS.Commands.SignInByEmail;

public record SignInByEmailCommand(string Email, string Password) : IRequest<Result<SignInByEmailResponse>>;