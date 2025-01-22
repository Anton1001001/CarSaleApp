namespace Car.Domain.Entities;
public class Generation
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public uint YearFrom { get; set; }
    public uint YearUntil { get; set; }
    public Guid ModelId { get; set; }
    public virtual Model Model { get; set; } = null!;
    public virtual ICollection<VariantOfExecution> VariantsOfExecution { get; set; } = new List<VariantOfExecution>();
}
