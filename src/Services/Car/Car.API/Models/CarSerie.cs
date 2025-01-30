namespace Car.API.Models;
public class CarSerie
{
    public int Id { get; set; }
    public int CarModelId { get; set; }
    public int CarBodyTypeId { get; set; }
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int? CarGenerationId { get; set; }
    public int CarTypeId { get; set; }
    public virtual ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
    public virtual CarBodyType CarBodyTypeNavigation { get; set; } = null!;
    public virtual CarGeneration? CarGenerationNavigation { get; set; }
    public virtual CarModel CarModelNavigation { get; set; } = null!;
    public virtual CarType CarTypeNavigation { get; set; } = null!;
}
