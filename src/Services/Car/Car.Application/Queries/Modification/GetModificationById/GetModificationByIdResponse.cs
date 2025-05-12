namespace Car.Application.Queries.Modification.GetModificationById;

public record GetModificationByIdResponse(
    int Id,
    string Name,
    int? EnginePower,
    int? EngineCapacity,
    decimal? GroundClearance,
    decimal? FuelConsumptionCombined);