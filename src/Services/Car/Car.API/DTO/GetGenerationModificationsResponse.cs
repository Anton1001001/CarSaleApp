namespace Car.API.DTO;
public record GetGenerationModificationsResponse(
    int Id,
    string Name,
    GetBodyTypesResponse BodyType,
    GetDriveTypesResponse? DriveType,
    GetTransmissionTypesResponse? TransmissionType,
    GetEngineTypesResponse? EngineType);