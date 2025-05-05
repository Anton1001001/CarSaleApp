using Chat.Core.Abstractions;
using Chat.Core.Errors.Base;
using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Queries.CheckDialogByAdvertId;

public class CheckDialogByAdvertIdHandler(IUnitOfWork unitOfWork, IAdvertServiceClient advertServiceClient)
    : IRequestHandler<CheckDialogByAdvertIdQuery, Result<CheckDialogByAdvertIdResponse>>
{
    public async Task<Result<CheckDialogByAdvertIdResponse>> Handle(CheckDialogByAdvertIdQuery request,
        CancellationToken cancellationToken)
    {
        if (request.UserId is null)
        {
            return new UnauthorizedError(code: "Unauthorized",
                message: "Не удалось получить идентификатор пользователя");
        }

        if (!Guid.TryParse(request.UserId, out var userId))
        {
            return new BadRequestError(code: "InvalidUserId", message: "Идентификатор пользователя имеет неверный формат");
        }

        var advertInfo = await advertServiceClient
            .GetAdvertPreviewByIdAsync(request.AdvertId, cancellationToken);

        if (advertInfo is null)
        {
            return new NotFoundError(code: "NotFound",
                $"Объявление с идентификатором {request.AdvertId} не было найдено");
        }
        
        var advertSellerId = Guid.Parse(advertInfo.SellerId!);

        if (advertSellerId.Equals(userId))
        {
            return new BadRequestError(code: "InvalidInput", message: "Вы не можете отправлять сообщения самому себе");
        }

        var dialog = await unitOfWork.DialogRepository.GetDialogAsync(
            request.AdvertId,
            advertSellerId,
            userId, cancellationToken);

        var response = dialog is not null
            ? new CheckDialogByAdvertIdResponse(
                advertInfo,
                dialog.Id)
            : new CheckDialogByAdvertIdResponse(advertInfo);
        
        return response;
    }
}