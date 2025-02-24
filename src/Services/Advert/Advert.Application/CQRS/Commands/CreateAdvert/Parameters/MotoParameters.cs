using Advert.Domain.Enums;
using Newtonsoft.Json;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Parameters;

public class MotoParameters : ParametersBase
{
    [JsonProperty("category")]
    public int CategoryTypeId { get; set; }
    
    [JsonProperty("moto-brand")]
    public int BrandId { get; set; }
    
    [JsonProperty("moto-model")]
    public int ModelId { get; set; }
    
    public int Year { get; set; }
    
    [JsonProperty("moto-type")]
    public int TypeId { get; set; }
    
    [JsonProperty("moto-engine")]
    public int EngineTypeId { get; set; }
    
    [JsonProperty("volume")]
    public int EngineCapacity { get; set; }
    
    [JsonProperty("power")]
    public int EnginePower { get; set; }
    
    [JsonProperty("moto-strokes")]
    public int EngineStroke { get; set; }
    
    [JsonProperty("cylinders-count")]
    public int CylinderNumber { get; set; }
    
    [JsonProperty("moto-cylinders-type")]
    public int CylindersTypeId { get; set; }
    
    [JsonProperty("moto-drive-unit")]
    public int DriveTypeId { get; set; }
    
    [JsonProperty("condition")]
    public int ConditionId { get; set; }
    
    [JsonProperty("moto-transmission")]
    public int TransmissionTypeId { get; set; }
    
    [JsonProperty("color")]
    public int ColorId { get; set; }
    
    [JsonProperty("mileage-unit")]
    public MileageUnit MileageUnit { get; set; }
    
    [JsonProperty("run")]
    public int Mileage { get; set; }
}
