using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Commands.CreateMessage;

public record CreateMessageCommand(string? UserId, int DialogId, string Text) : IRequest<Result<CreateMessageResponse>>;