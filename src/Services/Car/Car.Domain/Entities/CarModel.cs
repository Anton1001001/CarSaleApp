namespace Car.Domain.Entities;

public class CarModel
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public string Slug { get; set; } = null!;

    public int CarBrandId { get; set; }

    public CarBrand CarBrand { get; set; } = null!;

    public ICollection<CarGeneration> CarGenerations { get; set; } = new List<CarGeneration>();
}