namespace Car.Domain.Entities;
public class CarBrand
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public ICollection<CarModel> CarModels { get; set; } = new List<CarModel>();
}
