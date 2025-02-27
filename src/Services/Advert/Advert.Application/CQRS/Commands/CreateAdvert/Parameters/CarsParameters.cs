using Advert.Domain.Enums;
using Newtonsoft.Json;

namespace Advert.Application.CQRS.Commands.CreateAdvert.Parameters;

public class CarsParameters : ParametersBase
{
    [JsonProperty("cars-brand")] 
    public int BrandId { get; set; }

    [JsonProperty("cars-model")] 
    public int ModelId { get; set; }

    public int Year { get; set; }

    [JsonProperty("cars-body")] 
    public int BodyTypeId { get; set; }

    [JsonProperty("cars-generation")] 
    public int GenerationId { get; set; }

    [JsonProperty("cars-modification")] 
    public int ModificationId { get; set; }

    [JsonProperty("cars-engine")] 
    public int EngineTypeId { get; set; }

    [JsonProperty("cars-drive-unit")] 
    public int DriveTypeId { get; set; }

    [JsonProperty("cars-transmission")] 
    public int TransmissionTypeId { get; set; }

    [JsonProperty("engine_volume")] 
    public int EngineCapacity { get; set; }

    [JsonProperty("color")] 
    public int ColorId { get; set; }

    [JsonProperty("cars-condition")] 
    public int ConditionId { get; set; }

    [JsonProperty("mileage-unit")] 
    public MileageUnit MileageUnit { get; set; }

    [JsonProperty("run")] 
    public int Mileage { get; set; }

    public string? Vin { get; set; }

    [JsonProperty("interior-material")]
    public int InteriorMaterialId { get; set; }

    [JsonProperty("interior-color")]
    public int InteriorColorId { get; set; }

    [JsonProperty("registration-country")]
    public int RegistrationCountryId { get; set; }

    [JsonProperty("registration-status")]
    public RegistrationStatus RegistrationStatus { get; set; }

    public CarsParameters()
    {
        AdvertType = "cars";
    }
}

