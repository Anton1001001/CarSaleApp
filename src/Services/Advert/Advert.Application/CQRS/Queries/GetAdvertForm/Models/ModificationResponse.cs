namespace Advert.Application.CQRS.Queries.GetAdvertForm.Models;

public record ModificationResponse(
    int Id, 
    string Name, 
    BodyTypeResponse BodyType, 
    TransmissionTypeResponse TransmissionType, 
    DriveTypeResponse DriveType, 
    EngineTypeResponse EngineType
);
