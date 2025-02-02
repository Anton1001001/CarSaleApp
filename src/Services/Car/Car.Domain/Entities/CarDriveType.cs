namespace Car.Domain.Entities;

public class CarDriveType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
}