namespace Car.Domain.Entities;

public class CarModification
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CarBodyTypeId { get; set; }

    public int CarGenerationId { get; set; }

    public int? CarEngineTypeId { get; set; }

    public int? CarDriveTypeId { get; set; }

    public int? CarTransmissionTypeId { get; set; }

    public int? EngineCapacity { get; set; }

    public int? EnginePower { get; set; }

    public decimal? FuelConsumptionCombined { get; set; }

    public decimal? GroundClearance { get; set; }

    public CarBodyType CarBodyType { get; set; } = null!;

    public CarDriveType? CarDriveType { get; set; }

    public CarEngineType? CarEngineType { get; set; }

    public CarGeneration CarGeneration { get; set; } = null!;

    public CarTransmissionType? CarTransmissionType { get; set; }
}