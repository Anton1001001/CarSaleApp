namespace Car.Domain.Entities;

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
    public ICollection<CarOptionValue> CarOptionValues { get; set; } = new List<CarOptionValue>();
    public CarModification CarModificationNavigation { get; set; } = null!;
    public CarType CarTypeNavigation { get; set; } = null!;
}