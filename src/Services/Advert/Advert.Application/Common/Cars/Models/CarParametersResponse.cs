namespace Advert.Application.Common.Cars.Models;

public record CarParametersResponse(
    string Brand,
    string Model,
    string BodyType,
    string Generation,
    string Modification,
    string EngineType,
    string DriveType,
    string TransmissionType,
    string Color,
    string Condition,
    string InteriorMaterial,
    string InteriorColor,
    int Year,
    int MileageKm,
    string? Registration
) : IVehicleParametersResponse;
