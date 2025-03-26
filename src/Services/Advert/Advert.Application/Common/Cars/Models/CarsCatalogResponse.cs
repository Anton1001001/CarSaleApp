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
    string EngineCapacity,
    string Color,
    string Condition,
    string InteriorMaterial,
    string InteriorColor);
