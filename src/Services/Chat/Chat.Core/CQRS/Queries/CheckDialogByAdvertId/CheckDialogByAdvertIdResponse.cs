using Chat.Core.CQRS.Queries.GetUserDialogs;

namespace Chat.Core.CQRS.Queries.CheckDialogByAdvertId;

public record CheckDialogByAdvertIdResponse(
    AdvertPreviewResponse AdvertInfo,
    int? Id = null);
    