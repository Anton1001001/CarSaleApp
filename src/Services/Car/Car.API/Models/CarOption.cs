namespace Car.API.Models;
public class CarOption
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int? ParentId { get; set; }
    public uint DateCreate { get; set; }
    public uint DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public virtual ICollection<CarOptionValue> CarOptionValues { get; set; } = new List<CarOptionValue>();
    public virtual CarType CarTypeNavigation { get; set; } = null!;
    public virtual CarOption? ParentNavigation { get; set; }
    public virtual ICollection<CarOption> InverseParentNavigation { get; set; } = new List<CarOption>();
}
