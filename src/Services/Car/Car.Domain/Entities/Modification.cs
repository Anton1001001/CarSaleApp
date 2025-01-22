namespace Car.Domain.Entities;

public class Modification
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
