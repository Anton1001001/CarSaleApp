using Chat.Core.Abstractions;
using Chat.Core.Entities;
using Chat.Core.Errors.Base;
using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Commands.CreateDialog;

public class CreateDialogHandler(IUnitOfWork unitOfWork, IAdvertServiceClient advertServiceClient)
    : IRequestHandler<CreateDialogCommand, Result<CreateDialogResponse>>
{
    public async Task<Result<CreateDialogResponse>> Handle(CreateDialogCommand request,
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

        var advertPreview = await advertServiceClient.GetAdvertPreviewByIdAsync(request.AdvertId, cancellationToken);

        if (advertPreview is null)
        {
            return new NotFoundError(code: "NotFound",
                $"Объявление с идентификатором {request.AdvertId} не было найдено");
        }

        if (!Guid.TryParse(advertPreview.SellerId, out var sellerId))
        {
            return new BadRequestError(code: "InvalidUserId",
                message: "Идентификатор владельца объявления имеет неверный формат");
        }
        
        if (sellerId.Equals(userId))
        {
            return new BadRequestError(code: "InvalidInput", message: "Вы не можете отправлять сообщения самому себе");
        }

        var lastMessage = request.Text;
        var lastMessageTime = DateTime.UtcNow;
        var name = request.Name!;
        
        var dialog = new Dialog
        {
            AdvertId = advertPreview.Id,
            BuyerId = userId,
            SellerId = sellerId,
            Name = name,
            LastMessage = lastMessage,
            LastMessageTime = lastMessageTime
        };

        var message = new Message
        {
            Dialog = dialog,
            DialogId = dialog.Id,
            SenderId = userId,
            Text = lastMessage,
            SentAt = lastMessageTime
        };
        
        await unitOfWork.DialogRepository.CreateAsync(dialog, cancellationToken);
        await unitOfWork.MessageRepository.CreateAsync(message, cancellationToken);
        
        var saveResult = await unitOfWork.SaveChangesAsync(cancellationToken);

        if (!saveResult)
        {
            return new InternalServerError(
                code: "dialog_message_save_failed",
                message: "Ошибка при сохранении диалога. Попробуйте позже."
            );
        }
        
        var response = new CreateDialogResponse(advertPreview, dialog.Id, dialog.Name);

        return response;
    }
}