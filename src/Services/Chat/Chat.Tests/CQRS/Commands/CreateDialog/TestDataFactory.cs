using Chat.Core.CQRS.Commands.CreateDialog;
using Chat.Core.CQRS.Queries.GetUserDialogs;
using Chat.Core.Entities;

namespace Chat.Tests.CQRS.Commands.CreateDialog;

public static class TestDataFactory
{
    public static CreateDialogCommand CreateValidDialogCommand() =>
        new("b1565f4c-b2d3-4ea2-b6e9-b47d50f9cafa", "Test User", 1, "Hello");
    

    public static AdvertPreviewResponse CreateAdvertPreviewResponse() =>
        new(
            Id: 1,
            SellerId: "5d1f8c5d-9e57-4e1d-b90d-25cdb1e91773",
            AdvertType: "sell",
            PublicStatus: new("Active", "Активно"),
            AdvertStatus: "open",
            SellerName: "John Doe",
            Price: new PriceResponse(10000, 30000, 900000, 9500),
            PhotoPreviewUrl: "http://photo.url",
            Brand: "BMW",
            Model: "X5",
            Generation: "G05",
            Year: 2022
        );
    
    public static Dialog CreateDialog() =>
        new()
        {
            Id = 1,
            BuyerId = Guid.NewGuid(),
            SellerId = Guid.NewGuid(),
            Name = "Test",
            LastMessage = "Hi",
            LastMessageTime = DateTime.UtcNow,
            AdvertId = 1
        };

    public static Message CreateMessage(Dialog dialog) =>
        new()
        {
            DialogId = dialog.Id,
            Dialog = dialog,
            SenderId = dialog.BuyerId,
            Text = dialog.LastMessage,
            SentAt = dialog.LastMessageTime
        };

}
