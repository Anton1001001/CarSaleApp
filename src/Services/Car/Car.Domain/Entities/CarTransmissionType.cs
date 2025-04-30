namespace Car.Domain.Entities;

public class CarTransmissionType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
}
