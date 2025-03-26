using Advert.Domain.Enums;

namespace Advert.Application.Common.Advert.Models.Parameters;

public record MotoParameters(
    int CategoryTypeId,
    int BrandId,
    int ModelId,
    int Year,
    int TypeId,
    int EngineTypeId,
    int EngineCapacity,
    int EnginePower,
    int EngineStroke,
    int CylinderNumber,
    int CylindersTypeId,
    int DriveTypeId,
    int ConditionId,
    int TransmissionTypeId,
    int ColorId,
    MileageUnit MileageUnit,
    int Mileage
) : ParametersBase(Domain.Constants.AdvertType.Moto);

