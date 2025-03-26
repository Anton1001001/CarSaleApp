namespace Advert.Application.Common.Advert.Models;

public record AdvertResponse(
    int Id,
    string AdvertType,
    PublicStatusResponse PublicStatus,
    PrivateStatusResponse PrivateStatus,
    string AdvertStatus,
    string Description,
    int Version,
    DateTime PublishedAt,
    DateTime RefreshedAt,
    string? VideoUrl,
    string LocationName,
    string? SellerName,
    PriceResponse? Price,
    List<PhotoResponse> Photos,
    string? RemoveReason,
    IVehicleParametersResponse? Parameters,
    string ShortLocationName
);
