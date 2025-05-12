using Chat.Core.CQRS.Queries.GetUserDialogs;
using Chat.Core.Entities;

namespace Chat.Tests.CQRS.Queries.GetUserDialogs;

public static class TestDataFactory
{
    public static Dialog CreateDialog(int id, Guid userId, int advertId) =>
        new()
        {
            Id = id,
            AdvertId = advertId,
            BuyerId = userId,
            SellerId = Guid.NewGuid(),
            Name = "Test dialog",
            LastMessage = "Hello!",
            LastMessageTime = DateTime.UtcNow,
            Messages = new List<Message>()
        };

    public static AdvertPreviewResponse CreateAdvertPreview(int advertId, string sellerId) =>
        new(
            Id: advertId,
            SellerId: sellerId,
            AdvertType: "car",
            PublicStatus: new PublicStatusResponse("label", "name"),
            AdvertStatus: "active",
            SellerName: "Test Seller",
            Price: new PriceResponse(1000, 3000, 25000, 900),
            PhotoPreviewUrl: null,
            Brand: "BMW",
            Model: "X5",
            Generation: "G05",
            Year: 2022
        );
}

