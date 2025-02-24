namespace Advert.Domain.Entities;

public class PhoneCode
{
    public uint Id { get; set; }
    public string Label { get; set; } = null!;
    public string Emoji { get; set; } = null!;
    public string Code { get; set; } = null!;
    public ICollection<AdvertPhoneNumber> AdvertPhoneNumbers { get; set; } = new List<AdvertPhoneNumber>();
}