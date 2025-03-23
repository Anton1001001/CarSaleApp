using Advert.Domain.Enums;

namespace Advert.Application.Common.Advert.Models.Parameters;

public abstract record ParametersBase(
    string AdvertType,
    string? Description = null,
    PhotoRequest? Photos = null,
    int? Price = null,
    Currency? Currency = null,
    string? SellerName = null,
    string? Email = null,
    List<PhoneNumberRequest>? Phones = null,
    int? PlaceCityId = null,
    int? PlaceRegionId = null,
    int? PlaceCountryId = null
);