using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Commands.CreateDialog;

public record CreateDialogCommand(string? UserId, string? Name, int AdvertId, string Text) : IRequest<Result<CreateDialogResponse>>;