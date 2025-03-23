namespace Advert.Application.Common.Cars.Models;

public record CarsCatalogRequest(
    int BrandId,
    int ModelId,
    int BodyTypeId,
    int GenerationId,
    int ModificationId,
    int EngineTypeId,
    int DriveTypeId,
    int TransmissionTypeId,
    int ColorId,
    int ConditionId,
    int InteriorMaterialId,
    int InteriorColorId);
