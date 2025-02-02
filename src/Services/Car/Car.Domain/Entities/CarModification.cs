namespace Car.Domain.Entities;

public class CarModification
{
    public int Id { get; set; }
    public int CarSerieId { get; set; }
    public int CarModelId { get; set; }
    public string Name { get; set; } = null!;
    public uint? DateCreate { get; set; }
    public uint? DateUpdate { get; set; }
    public int CarTypeId { get; set; }
    public int? CarEngineTypeId { get; set; }
    public int? CarDriveTypeId { get; set; }
    public int? CarTransmissionTypeId { get; set; }
    public int? EngineCapacity { get; set; }
    public int? EnginePower { get; set; }
    public decimal? FuelConsumptionCombined { get; set; }
    public decimal? GroundClearance { get; set; }

    public ICollection<CarCharacteristicValue> CarCharacteristicValues { get; set; } =
        new List<CarCharacteristicValue>();

    public ICollection<CarEquipment> CarEquipment { get; set; } = new List<CarEquipment>();
    public CarDriveType? CarDriveTypeNavigation { get; set; }
    public CarEngineType? CarEngineTypeNavigation { get; set; }
    public CarModel CarModelNavigation { get; set; } = null!;
    public CarSerie CarSerieNavigation { get; set; } = null!;
    public CarTransmissionType? CarTransmissionTypeNavigation { get; set; }
    public CarType CarTypeNavigation { get; set; } = null!;
}