namespace Advert.Application.Common.Cars.Models;

public record CarParametersResponse(
    string Brand,
    string Model,
    string Generation,
    int Year,
    string EngineType,
    string TransmissionType,
    string GenerationWithYears,
    string InteriorMaterial,
    string InteriorColor,
    string BodyType,
    string DriveType,
    string Modification,
    string Color,
    int MileageKm,
    string Condition,
    string? Registration
) : IVehicleParametersResponse;
