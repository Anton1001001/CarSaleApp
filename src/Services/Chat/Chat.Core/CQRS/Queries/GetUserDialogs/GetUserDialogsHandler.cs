using Chat.Core.Abstractions;
using Chat.Core.Errors.Base;
using FluentResults;
using MediatR;

namespace Chat.Core.CQRS.Queries.GetUserDialogs;

public class GetUserDialogsHandler(IUnitOfWork unitOfWork, IAdvertServiceClient advertServiceClient)
    : IRequestHandler<GetUserDialogsQuery, Result<List<GetUserDialogsResponse>>>
{
    public async Task<Result<List<GetUserDialogsResponse>>> Handle(GetUserDialogsQuery request,
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

        var dialogs = await unitOfWork.DialogRepository
            .GetDialogsByUserIdAsync(userId, cancellationToken);

        var advertsIds = dialogs.Select(d => d.AdvertId).ToList();

        var advertsPreviews = await advertServiceClient
            .GetAdvertsPreviewsByIdsAsync(advertsIds, cancellationToken);

        var response = dialogs
            .Select(dialog =>
            {
                var advertInfo = advertsPreviews
                    .FirstOrDefault(a => a.Id == dialog.AdvertId);
        
                var sellerId = Guid.Parse(advertInfo?.SellerId!);
                var isAdvertOwner = sellerId.Equals(userId);
                return new GetUserDialogsResponse(
                    advertInfo!,
                    dialog.Name,
                    dialog.Id,
                    isAdvertOwner,
                    dialog.LastMessageTime,
                    dialog.LastMessage
                );
            })
            .ToList();

        return response;
    }
}