namespace Advert.Domain.Entities;

public class AdvertPublicStatus
{
    public uint Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Label { get; set; }
    public ICollection<Advert> Adverts { get; set; } = new List<Advert>();
}