namespace Car.Domain.Entities;

public class CarGeneration
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CarModelId { get; set; }
    public string? YearBegin { get; set; }
    public string? YearEnd { get; set; }
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public ICollection<CarSerie> CarSeries { get; set; } = new List<CarSerie>();
    public CarModel CarModelNavigation { get; set; } = null!;
    public CarType CarTypeNavigation { get; set; } = null!;
}