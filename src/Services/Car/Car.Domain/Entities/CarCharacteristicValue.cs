namespace Car.Domain.Entities;

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
    public CarCharacteristic CarCharacteristicNavigation { get; set; } = null!;
    public CarModification CarModificationNavigation { get; set; } = null!;
    public CarType CarTypeNavigation { get; set; } = null!;
}