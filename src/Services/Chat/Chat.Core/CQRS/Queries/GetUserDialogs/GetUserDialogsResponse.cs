namespace Chat.Core.CQRS.Queries.GetUserDialogs;

public record GetUserDialogsResponse(
    AdvertPreviewResponse AdvertInfo, 
    string Name,
    int DialogId,
    bool IsAdvertOwner,
    DateTime LastMessageTime,
    string LastMessage);