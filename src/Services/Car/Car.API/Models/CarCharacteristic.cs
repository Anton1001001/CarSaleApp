namespace Car.API.Models;
public class CarCharacteristic
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? ParentId { get; set; }
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public virtual ICollection<CarCharacteristicValue> CarCharacteristicValues { get; set; } = new List<CarCharacteristicValue>();
    public virtual CarType CarTypeNavigation { get; set; } = null!;
    public virtual CarCharacteristic? ParentNavigation { get; set; }
    public virtual ICollection<CarCharacteristic> InverseParentNavigation { get; set; } = new List<CarCharacteristic>();
}
