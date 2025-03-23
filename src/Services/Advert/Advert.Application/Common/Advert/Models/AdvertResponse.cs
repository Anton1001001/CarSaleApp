using Advert.Application.CQRS.Commands.CreateAdvert;

namespace Advert.Application.Common.Advert.Models;

public record AdvertResponse(
    int Id,
    string AdvertType,
    PublicStatus PublicStatus,
    PrivateStatus PrivateStatus,
    string AdvertStatus,
    int DaysOnSale,
    string Description,
    int Version,
    DateTime PublishedAt,
    DateTime RefreshedAt,
    string? VideoUrl,
    string LocationName,
    string ShortLocationName,
    string? SellerName,
    PriceResponse Price,
    List<PhotoResponse> Photos,
    string? RemoveReason,
    IVehicleParametersResponse Parameters
);
