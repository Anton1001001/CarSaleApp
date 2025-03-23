using Advert.Domain.Enums;

namespace Advert.Application.Common.Advert.Models.Parameters;
public record BusesParameters(
    int BrandId,
    int ModelId,
    int Year,
    int BodyTypeId,
    int EngineTypeId,
    int EngineCapacity,
    int EnginePower,
    int WheelFormula,
    int TransmissionTypeId,
    int SeatsCount,
    MileageUnit MileageUnit,
    int Mileage
) : ParametersBase(Domain.Constants.AdvertType.Buses);

