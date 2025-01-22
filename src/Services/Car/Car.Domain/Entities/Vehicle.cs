namespace Car.Domain.Entities;

public class Vehicle
{
    public Guid Id { get; set; }
    public Guid ModificationId { get; set; }
    public Guid VariantOfExecutionId { get; set; }
    public virtual Modification Modification { get; set; } = null!;
    public virtual VariantOfExecution VariantOfExecution { get; set; } = null!;
}
