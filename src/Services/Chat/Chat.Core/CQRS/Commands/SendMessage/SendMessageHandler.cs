using Chat.Core.Abstractions;
using Chat.Core.Entities;
using Chat.Core.Errors.Base;
using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Commands.SendMessage;

public class SendMessageHandler(IUnitOfWork unitOfWork) : IRequestHandler<SendMessageCommand, Result<SendMessageResponse>>
{
    public async Task<Result<SendMessageResponse>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        if (request.UserId is null)
        {
            return new UnauthorizedError(code: "Unauthorized",
                message: "Не удалось получить идентификатор пользователя");
        }

        var userIdGuid = Guid.Parse(request.UserId);
        
        var dialog = await unitOfWork.DialogRepository.GetByIdAsync(request.DialogId, cancellationToken);

        if (dialog is null)
        {
            return new NotFoundError(code: "NotFound", message: "Dialog not found");
        }

        var message = new Message()
        {
            DialogId = dialog.Id,
            SenderId = userIdGuid,
            SentAt = DateTime.UtcNow,
            Text = request.Message
        };
        
        var saveResult = await unitOfWork.MessageRepository.CreateAsync(message, cancellationToken);
        return new SendMessageResponse();
    }
}