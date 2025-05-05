namespace Car.Domain.Entities;
public class CarGeneration
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CarModelId { get; set; }

    public int YearBegin { get; set; }

    public int? YearEnd { get; set; }

    public CarModel CarModel { get; set; } = null!;

    public ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
}
