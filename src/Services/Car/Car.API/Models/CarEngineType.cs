namespace Car.API.Models;
public class CarEngineType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<CarModification> CarModifications { get; set; } = new List<CarModification>();
}
