namespace Car.Domain.Entities;

public class CarCharacteristic
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? ParentId { get; set; }
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarTypeId { get; set; }

    public ICollection<CarCharacteristicValue> CarCharacteristicValues { get; set; } =
        new List<CarCharacteristicValue>();

    public CarType CarTypeNavigation { get; set; } = null!;
    public CarCharacteristic? ParentNavigation { get; set; }
    public ICollection<CarCharacteristic> InverseParentNavigation { get; set; } = new List<CarCharacteristic>();
}