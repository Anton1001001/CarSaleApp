namespace Car.API.Models;
public class CarOption
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    public uint DateCreate { get; set; }
    public uint DateUpdate { get; set; }
    public int CarTypeId { get; set; }
}
