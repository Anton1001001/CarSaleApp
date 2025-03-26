namespace Advert.Domain.Entities;

public class AdvertPhoneNumber
{
    public uint Id { get; set; }
    public int AdvertId { get; set; }
    public int PhoneCodeId { get; set; }
    public string Number { get; set; } = null!;
    public Advert Advert { get; set; } = null!;
    public PhoneCode PhoneCode { get; set; } = null!;
}