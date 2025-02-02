namespace Car.Domain.Entities;

public class CarOption
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    public uint DateCreate { get; set; }
    public uint DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public ICollection<CarOptionValue> CarOptionValues { get; set; } = new List<CarOptionValue>();
    public CarType CarTypeNavigation { get; set; } = null!;
    public CarOption? ParentNavigation { get; set; }
    public ICollection<CarOption> InverseParentNavigation { get; set; } = new List<CarOption>();
}