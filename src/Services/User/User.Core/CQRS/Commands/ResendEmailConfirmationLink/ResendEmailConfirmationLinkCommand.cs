using FluentResults;
using MediatR;

namespace User.Core.CQRS.Commands.ResendEmailConfirmationLink;

public record ResendEmailConfirmationLinkCommand(string Email) : IRequest<Result<ResendEmailConfirmationLinkResponse>>;