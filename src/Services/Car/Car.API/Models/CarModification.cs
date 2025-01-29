namespace Car.API.Models;
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
}
