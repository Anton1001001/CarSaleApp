namespace Advert.Domain.Entities;

public class AdvertPrivateStatus
{
    public uint Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Label { get; set; }
    public bool Published { get; set; }
    public string? PhotoLabel { get; set; }
    public ICollection<Advert> Adverts { get; set; } = new List<Advert>();
}