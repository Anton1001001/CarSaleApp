namespace Car.API.Models;
public class CarModel
{
    public int Id { get; set; }
    public int CarBrandId { get; set; }
    public string Name { get; set; } = null!;
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public string? NameRus { get; set; }
    public virtual ICollection<CarGeneration> CarGenerations { get; set; } = new List<CarGeneration>();
    public virtual ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
    public virtual ICollection<CarSerie> CarSeries { get; set; } = new List<CarSerie>();
    public virtual CarBrand CarBrandNavigation { get; set; } = null!;
    public virtual CarType CarTypeNavigation { get; set; } = null!;
}
