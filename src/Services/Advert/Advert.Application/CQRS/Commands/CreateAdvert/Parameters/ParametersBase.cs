using Advert.Domain.Enums;
using Newtonsoft.Json;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Parameters;

public abstract class ParametersBase
{
    public string AdvertType { get; set; }
    public string? Description { get; set; }
    public PhotoRequest? Photos { get; set; }

    public int Price { get; set; }

    public Currency Currency { get; set; }

    [JsonProperty("seller-name")] 
    public string SellerName { get; set; } = string.Empty;

    public string? Email { get; set; }

    public List<PhoneNumberRequest> Phones { get; set; } = [];

    [JsonProperty("place-city")]
    public int PlaceCityId { get; set; }
    
    [JsonProperty("place-region")]
    public int PlaceRegionId { get; set; }
    public int? PlaceCountryId { get; set; }
}