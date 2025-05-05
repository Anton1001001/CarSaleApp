using Chat.Core.Abstractions;
using Chat.Core.Errors.Base;
using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Queries.GetDialogMessages;

public class GetDialogMessagesHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<GetDialogMessagesQuery, Result<List<GetDialogMessageResponse>>>
{
    public async Task<Result<List<GetDialogMessageResponse>>> Handle(GetDialogMessagesQuery request,
        CancellationToken cancellationToken)
    {
        if (request.UserId is null)
        {
            return new UnauthorizedError(code: "Unauthorized",
                message: "Не удалось получить идентификатор пользователя");
        }

        if (!Guid.TryParse(request.UserId, out var userId))
        {
            return new BadRequestError(code: "InvalidUserId",
                message: "Идентификатор пользователя имеет неверный формат");
        }

        var messages = await unitOfWork.MessageRepository.GetByDialogIdAsync(request.DialogId, cancellationToken);

        var response = messages.Select(message =>
            new GetDialogMessageResponse(
                message.Id,
                message.SentAt,
                message.SenderId.Equals(userId),
                message.Text)).ToList();

        return response;
    }
}