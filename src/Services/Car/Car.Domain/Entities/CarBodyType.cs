namespace Car.Domain.Entities;

public class CarBodyType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<CarSerie> CarSeries { get; set; } = new List<CarSerie>();
}