namespace Car.API.Models;
public class CarOptionValue
{
    public int Id { get; set; }
    public bool? IsBase { get; set; }
    public int CarOptionId { get; set; }
    public int CarEquipmentId { get; set; }
    public uint DateCreate { get; set; }
    public uint DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public virtual CarEquipment CarEquipmentNavigation { get; set; } = null!;
    public virtual CarOption CarOptionNavigation { get; set; } = null!;
    public virtual CarType CarTypeNavigation { get; set; } = null!;
}
