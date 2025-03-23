using Advert.Domain.Enums;

namespace Advert.Application.Common.Cars.Models;

public record CarParametersToSave(
    int BrandId,
    int ModelId,
    int Year,
    int BodyTypeId,
    int GenerationId,
    int ModificationId,
    int EngineTypeId,
    int DriveTypeId,
    int TransmissionTypeId,
    int ColorId,
    int ConditionId,
    int MileageKm,
    string? Vin,
    int InteriorMaterialId,
    int InteriorColorId,
    int RegistrationCountryId,
    RegistrationStatus RegistrationStatus
);
