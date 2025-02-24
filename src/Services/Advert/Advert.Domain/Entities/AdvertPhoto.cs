namespace Advert.Domain.Entities;

public class AdvertPhoto
{
    public int AdvertId { get; set; }
    public int FileId { get; set; }
    public bool IsMain { get; set; }
    public Advert Advert { get; set; } = null!;
}