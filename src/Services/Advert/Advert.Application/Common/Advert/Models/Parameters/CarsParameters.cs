using Advert.Domain.Enums;

namespace Advert.Application.Common.Advert.Models.Parameters;

public record CarsParameters(
    int? BrandId,
    int? ModelId,
    int? Year,
    int? BodyTypeId,
    int? GenerationId,
    int? ModificationId,
    int? EngineTypeId,
    int? DriveTypeId,
    int? TransmissionTypeId,
    int? EngineCapacity,
    int? ColorId,
    int? ConditionId,
    MileageUnit? MileageUnit,
    int? Mileage,
    string? Vin,
    int? InteriorMaterialId,
    int? InteriorColorId,
    int? RegistrationCountryId,
    RegistrationStatus? RegistrationStatus
) : ParametersBase(Domain.Constants.AdvertType.Cars);

