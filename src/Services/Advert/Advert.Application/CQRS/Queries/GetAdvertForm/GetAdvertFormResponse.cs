using Advert.Application.CQRS.Queries.GetAdvertForm.Models;

namespace Advert.Application.CQRS.Queries.GetAdvertForm;

public record GetAdvertFormResponse(
    List<BrandResponse>? Brands,
    List<ColorResponse>? Colors,
    List<InteriorColorResponse>? InteriorColors,
    List<InteriorMaterialResponse>? InteriorMaterials,
    List<PlaceRegionResponse>? PlaceRegions,
    List<PhoneCodeResponse>? PhoneCodes,
    List<ModelResponse>? Models = null,
    List<YearResponse>? Years = null,
    List<GenerationResponse>? Generations = null,
    List<BodyTypeResponse>? BodyTypes = null,
    List<TransmissionTypeResponse>? TransmissionTypes = null,
    List<EngineTypeResponse>? EngineTypes = null,
    List<DriveTypeResponse>? DriveTypes = null,
    List<ModificationResponse>? Modifications = null,
    List<PlaceCityResponse>? PlaceCities = null
);
