namespace Car.Domain.Entities;

public class CarModel
{
    public int Id { get; set; }
    public int CarBrandId { get; set; }
    public string Name { get; set; } = null!;
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public string? NameRus { get; set; }
    public ICollection<CarGeneration> CarGenerations { get; set; } = new List<CarGeneration>();
    public ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
    public ICollection<CarSerie> CarSeries { get; set; } = new List<CarSerie>();
    public CarBrand CarBrandNavigation { get; set; } = null!;
    public CarType CarTypeNavigation { get; set; } = null!;
}