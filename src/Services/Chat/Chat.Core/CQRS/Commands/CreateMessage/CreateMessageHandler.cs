using Chat.Core.Abstractions;
using Chat.Core.Entities;
using Chat.Core.Errors.Base;
using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Commands.CreateMessage;

public class CreateMessageHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateMessageCommand, Result<CreateMessageResponse>>
{
    public async Task<Result<CreateMessageResponse>> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
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

        var message = new Message
        {
            DialogId = request.DialogId,
            SenderId = userId,
            Text = request.Text,
            SentAt = DateTime.UtcNow
        };
        
        var dialog = await unitOfWork.DialogRepository.GetByIdAsync(request.DialogId, cancellationToken);

        if (dialog is null)
        {
            return new NotFoundError(
                code: "dialog_not_found",
                message: $"Диалог с идентификатором {request.DialogId} не найден."
            );
        }

        dialog.LastMessage = message.Text;
        dialog.LastMessageTime = message.SentAt;
        
        await unitOfWork.MessageRepository.CreateAsync(message, cancellationToken);
        
        var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (!saveResult)
        {
            return new InternalServerError(
                code: "failed_to_save_message",
                message: "Не удалось сохранить сообщение."
            );
        }

        var response = new CreateMessageResponse(message.Id, message.SentAt, message.SenderId, message.Text);

        return response;
    }
}