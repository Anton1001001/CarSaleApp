namespace Advert.Application.Common.Advert.Models;

public record AdvertPreviewResponse(
    int Id,
    string? SellerId,
    string AdvertType,
    PublicStatusResponse PublicStatus,
    string AdvertStatus,
    string SellerName,
    PriceResponse? Price,
    string? PhotoPreviewUrl,
    string Brand,
    string Model,
    string Generation,
    int Year);
    