using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Commands.SendMessage;

public record SendMessageCommand(string? UserId, int DialogId, string Message) : IRequest<Result<SendMessageResponse>>;