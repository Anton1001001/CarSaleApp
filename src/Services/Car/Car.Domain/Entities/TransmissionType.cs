namespace Car.Domain.Entities;
public class TransmissionType
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<VariantOfExecution> VariantsOfExecution { get; set; } = new List<VariantOfExecution>();
}
