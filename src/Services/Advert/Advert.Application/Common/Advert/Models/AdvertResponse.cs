using Advert.Application.CQRS.Commands.CreateAdvert;

namespace Advert.Application.Common.Advert.Models;

public class AdvertResponse
{
    public int Id { get; set; }
    public string AdvertType { get; set; }
    public PublicStatus PublicStatus { get; set; }
    public PrivateStatus PrivateStatus { get; set; }
    public string AdvertStatus { get; set; }
    public int DaysOnSale { get; set; }
    public string Description { get; set; }
    public int Version { get; set; }
    public DateTime PublishedAt { get; set; }
    public DateTime RefreshedAt { get; set; }
    public string? VideoUrl { get; set; }
    public string LocationName { get; set; }
    public string ShortLocationName { get; set; }
    public string? SellerName { get; set; }
    public PriceResponse Price { get; set; }
    public List<PhotoResponse> Photos { get; set; }
    public string? RemoveReason { get; set; }
    public IVehicleParametersResponse Parameters { get; set; }
}