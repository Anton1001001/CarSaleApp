namespace Car.Domain.Entities;

public class CarOptionValue
{
    public int Id { get; set; }
    public bool? IsBase { get; set; }
    public int CarOptionId { get; set; }
    public int CarEquipmentId { get; set; }
    public uint DateCreate { get; set; }
    public uint DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public CarEquipment CarEquipmentNavigation { get; set; } = null!;
    public CarOption CarOptionNavigation { get; set; } = null!;
    public CarType CarTypeNavigation { get; set; } = null!;
}