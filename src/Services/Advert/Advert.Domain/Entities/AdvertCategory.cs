namespace Advert.Domain.Entities;

public class AdvertCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Label { get; set; } = null!;

    public ICollection<Advert> Adverts { get; set; } = new List<Advert>();
}