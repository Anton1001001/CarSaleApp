using Chat.Core.CQRS.Queries.GetUserDialogs;

namespace Chat.Core.CQRS.Commands.CreateDialog;

public record CreateDialogResponse(
    AdvertPreviewResponse AdvertInfo, 
    int Id,
    string Name);