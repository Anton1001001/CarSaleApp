namespace Car.Domain.Entities;

public class CarSerie
{
    public int Id { get; set; }
    public int CarModelId { get; set; }
    public int CarBodyTypeId { get; set; }
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int? CarGenerationId { get; set; }
    public int CarTypeId { get; set; }
    public ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
    public CarBodyType CarBodyTypeNavigation { get; set; } = null!;
    public CarGeneration? CarGenerationNavigation { get; set; }
    public CarModel CarModelNavigation { get; set; } = null!;
    public CarType CarTypeNavigation { get; set; } = null!;
}