namespace Car.Domain.Entities;

public class CarEngineType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
}
