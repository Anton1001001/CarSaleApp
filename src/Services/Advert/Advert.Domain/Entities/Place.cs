namespace Advert.Domain.Entities;

public class Place
{
    public int Id { get; set; }
    public int? ParentId { get; set; }
    public string Type { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? ShortName { get; set; } = null!;
    public string Label { get; set; } = null!;
    public string? LabelBel { get; set; }
    public string? Emoji { get; set; }
    public string? CaseLabel { get; set; }
    public string? CaseLabelBel { get; set; }
    public ICollection<Advert> AdvertPlaceCities { get; set; } = new List<Advert>();
    public ICollection<Advert> AdvertPlaceCountries { get; set; } = new List<Advert>();
    public ICollection<Advert> AdvertPlaceRegions { get; set; } = new List<Advert>();
    public ICollection<Place> InverseParent { get; set; } = new List<Place>();
    public Place? Parent { get; set; }
}
