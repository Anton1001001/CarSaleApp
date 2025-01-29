namespace Car.API.Models;
public class CarEquipment
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public uint DateCreate { get; set; }
    public uint DateUpdate { get; set; }
    public int CarModificationId { get; set; }
    public int? PriceMin { get; set; }
    public int CarTypeId { get; set; }
    public int? Year { get; set; }
}
