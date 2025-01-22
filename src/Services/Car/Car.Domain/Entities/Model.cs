namespace Car.Domain.Entities;

public class Model
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public uint YearFrom { get; set; }
    public uint YearUntil { get; set; }
    public Guid BrandId { get; set; }
    public virtual Brand Brand { get; set; } = null!;
    public virtual ICollection<Generation> Generations { get; set; } = new List<Generation>();
}
