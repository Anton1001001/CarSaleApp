namespace Car.API.Models;
public class CarEquipment
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarModificationId { get; set; }
    public int? PriceMin { get; set; }
    public int CarTypeId { get; set; }
    public int? Year { get; set; }
    public virtual ICollection<CarOptionValue> CarOptionValues { get; set; } = new List<CarOptionValue>();
    public virtual CarModification CarModificationNavigation { get; set; } = null!;
    public virtual CarType CarTypeNavigation { get; set; } = null!;
}
