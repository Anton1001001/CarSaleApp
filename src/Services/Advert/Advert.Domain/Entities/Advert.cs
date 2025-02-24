using Advert.Domain.Enums;

namespace Advert.Domain.Entities;

public class Advert
{
    public int Id { get; set; }
    public string AdvertType { get; set; }
    public uint? AdvertPrivateStatusId { get; set; }
    public uint? AdvertPublicStatusId { get; set; }
    public string? AdvertStatus { get; set; }
    public int? DaysOnSale { get; set; }
    public string? Description { get; set; }
    public int? Version { get; set; }
    public DateTime? PublishedAt { get; set; }
    public DateTime? RefreshedAt { get; set; }
    public string? RemoveReason { get; set; }
    public string? VideoUrl { get; set; }
    public Currency? PriceCurrency { get; set; }
    public int? PriceAmount { get; set; }
    public int? TodayViews { get; set; }
    public int? TotalViews { get; set; }
    public string? SellerName { get; set; }
    public int? TotalPhoneViews { get; set; }
    public int? TotalBookmarks { get; set; }
    public int? TotalVinViews { get; set; }
    public DateTime? NextRefreshAvailableAt { get; set; }
    public uint? PlaceCountryId { get; set; }
    public uint? PlaceRegionId { get; set; }
    public uint? PlaceCityId { get; set; }
    public string? Properties { get; set; }
    public ICollection<AdvertPhoneNumber> AdvertPhoneNumbers { get; set; }
    public ICollection<AdvertPhoto>? AdvertPhotos { get; set; }
    public AdvertPrivateStatus? AdvertPrivateStatus { get; set; }
    public AdvertPublicStatus? AdvertPublicStatus { get; set; }
    public Place? PlaceCity { get; set; }
    public Place? PlaceCountry { get; set; }
    public Place? PlaceRegion { get; set; }

    public Advert()
    {
        DaysOnSale = 0;
        Version = 0;
        PublishedAt = DateTime.Now;
        RefreshedAt = DateTime.Now;
        RemoveReason = null;
        TodayViews = 0;
        TotalViews = 0;
        TotalPhoneViews = 0;
        TotalBookmarks = 0;
        TotalVinViews = 0;
        NextRefreshAvailableAt = RefreshedAt.Value.AddHours(12);
        AdvertPhoneNumbers = new List<AdvertPhoneNumber>();
    }
}
