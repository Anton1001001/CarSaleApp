namespace Car.Application.Queries.GetGenerationModifications;

public record GetGenerationModificationsResponse(
    int Id,
    string Name,
    EngineTypeResponse? EngineType,
    DriveTypeResponse? DriveType,
    TransmissionTypeResponse? TransmissionType,
    BodyTypeResponse BodyType);