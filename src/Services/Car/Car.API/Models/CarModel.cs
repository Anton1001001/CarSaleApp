namespace Car.API.Models;
public class CarModel
{
    public int Id { get; set; }
    public int CarBrandId { get; set; }
    public string Name { get; set; } = null!;
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public string? NameRus { get; set; }
}
