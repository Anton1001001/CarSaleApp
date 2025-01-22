namespace Car.Domain.Entities;
public class VariantOfExecution
{
    public Guid Id { get; set; }
    public Guid EngineTypeId { get; set; }
    public Guid DriveTypeId { get; set; }
    public Guid TransmissionTypeId { get; set; }
    public Guid BodyTypeId { get; set; }
    public Guid GenerationId { get; set; }
    public virtual BodyType BodyType { get; set; } = null!;
    public virtual DriveType DriveType { get; set; } = null!;
    public virtual EngineType EngineType { get; set; } = null!;
    public virtual Generation Generation { get; set; } = null!;
    public virtual TransmissionType TransmissionType { get; set; } = null!;
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
