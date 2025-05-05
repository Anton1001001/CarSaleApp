using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Queries.GetDialogMessages;

public record GetDialogMessagesQuery(int DialogId, string? UserId) : IRequest<Result<List<GetDialogMessageResponse>>>;