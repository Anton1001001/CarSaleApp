using Chat.Core.Entities;
using Chat.Core.CQRS.Queries.CheckDialogByAdvertId;
using Chat.Core.CQRS.Queries.GetUserDialogs;

namespace Chat.Tests.CQRS.Queries.CheckDialogByAdvertId;

public static class TestDataFactory
{
    public static Guid BuyerId => Guid.Parse("11111111-1111-1111-1111-111111111111");
    public static Guid SellerId => Guid.Parse("22222222-2222-2222-2222-222222222222");
    public static int AdvertId => 99;

    public static CheckDialogByAdvertIdQuery ValidQuery() =>
        new(BuyerId.ToString(), AdvertId);

    public static CheckDialogByAdvertIdQuery QueryWithNullUserId() =>
        new(null, AdvertId);

    public static CheckDialogByAdvertIdQuery QueryWithInvalidUserId() =>
        new("invalid-guid", AdvertId);

    public static AdvertPreviewResponse CreateAdvertPreviewResponse(bool fromSeller = false)
    {
        return new AdvertPreviewResponse(
            Id: AdvertId,
            SellerId: fromSeller ? BuyerId.ToString() : SellerId.ToString(),
            AdvertType: "sell",
            PublicStatus: new PublicStatusResponse("Активно", "active"),
            AdvertStatus: "published",
            SellerName: "Test Seller",
            Price: new PriceResponse(10000, 30000, 1000000, 9000),
            PhotoPreviewUrl: "http://image.url",
            Brand: "BMW",
            Model: "X5",
            Generation: "G05",
            Year: 2020
        );
    }

    public static Dialog CreateDialog() =>
        new()
        {
            Id = 10,
            AdvertId = AdvertId,
            BuyerId = BuyerId,
            SellerId = SellerId
        };
}