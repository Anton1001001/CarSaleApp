using Advert.Domain.Enums;
using Newtonsoft.Json;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Parameters;

public class BusesParameters : ParametersBase
{
    [JsonProperty("buses-brand")] 
    public int BrandId { get; set; }

    [JsonProperty("buses-model")] 
    public int ModelId { get; set; }

    public int Year { get; set; }

    [JsonProperty("buses-body")] 
    public int BodyTypeId { get; set; }

    [JsonProperty("buses-engine")] 
    public int EngineTypeId { get; set; }

    [JsonProperty("volume")] 
    public int EngineCapacity { get; set; }

    [JsonProperty("power")] 
    public int EnginePower { get; set; }

    [JsonProperty("buses-wheel-drive")] 
    public int WheelFormula { get; set; }

    [JsonProperty("buses-transmission")]
    public int TransmissionTypeId { get; set; }
    
    [JsonProperty("seats")]
    public int SeatsCount { get; set; }
    
    [JsonProperty("mileage-unit")]
    public MileageUnit MileageUnit { get; set; }
    
    [JsonProperty("run")]
    public int Mileage { get; set; }
}
