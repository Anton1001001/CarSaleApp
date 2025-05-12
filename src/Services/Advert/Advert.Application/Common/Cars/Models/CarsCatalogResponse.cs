namespace Advert.Application.Common.Cars.Models;

public record CarsCatalogResponse(
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
    int? EngineCapacity,
    int? EnginePower,
    float? FuelConsumptionCombined,
    float? GroundClearance);
