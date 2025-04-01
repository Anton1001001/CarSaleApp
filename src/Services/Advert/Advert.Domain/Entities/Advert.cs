using Advert.Domain.Enums;

namespace Advert.Domain.Entities;

public class Advert
{
    public int Id { get; set; }
    public string AdvertType { get; set; } = null!;
    public uint AdvertPrivateStatusId { get; set; }
    public uint AdvertPublicStatusId { get; set; }
    public string AdvertStatus { get; set; }
    public int DaysOnSale { get; set; } = 0;
    public string Description { get; set; }
    public int Version { get; set; } = 0;
    public DateTime PublishedAt { get; set; } = DateTime.Now;
    public DateTime RefreshedAt { get; set; }
    public string? RemoveReason { get; set; } = null;
    public string? VideoUrl { get; set; }
    public Currency PriceCurrency { get; set; }
    public int PriceAmount { get; set; }
    public int TodayViews { get; set; } = 0;
    public int TotalViews { get; set; } = 0;
    public string SellerName { get; set; }
    public int TotalPhoneViews { get; set; } = 0;
    public int TotalBookmarks { get; set; } = 0;
    public int TotalVinViews { get; set; } = 0;
    public DateTime NextRefreshAvailableAt { get; set; }
    public int? PlaceCountryId { get; set; }
    public int PlaceRegionId { get; set; }
    public int PlaceCityId { get; set; }
    public string Properties { get; set; }
    public int AdvertCategoryId { get; set; }
    public AdvertCategory AdvertCategory { get; set; } = null!;
    public ICollection<AdvertPhoneNumber> AdvertPhoneNumbers { get; set; } = [];
    public ICollection<AdvertPhoto>? AdvertPhotos { get; set; }
    public AdvertPrivateStatus AdvertPrivateStatus { get; set; }
    public AdvertPublicStatus AdvertPublicStatus { get; set; }
    public Place PlaceCity { get; set; }
    public Place? PlaceCountry { get; set; }
    public Place PlaceRegion { get; set; }

    public void Refresh()
    {
        RefreshedAt = DateTime.Now;
        NextRefreshAvailableAt = RefreshedAt.AddHours(12);
    }

    public Advert()
    {
        Refresh();
    }
}
