namespace Car.API.Models;
public class CarBodyType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<CarSerie> CarSeries { get; set; } = new List<CarSerie>();
}
