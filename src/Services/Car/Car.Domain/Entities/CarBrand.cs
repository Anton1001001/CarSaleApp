namespace Car.Domain.Entities;

public class CarBrand
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public string? NameRus { get; set; }
    public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
    public CarType CarTypeNavigation { get; set; } = null!;
}