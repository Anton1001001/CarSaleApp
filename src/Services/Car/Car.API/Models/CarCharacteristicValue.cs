namespace Car.API.Models;
public class CarCharacteristicValue
{
    public int Id { get; set; }
    public string? Value { get; set; }
    public string? Unit { get; set; }
    public int CarCharacteristicId { get; set; }
    public int CarModificationId { get; set; }
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public virtual CarCharacteristic CarCharacteristicNavigation { get; set; } = null!;
    public virtual CarModification CarModificationNavigation { get; set; } = null!;
    public virtual CarType CarTypeNavigation { get; set; } = null!;
}
