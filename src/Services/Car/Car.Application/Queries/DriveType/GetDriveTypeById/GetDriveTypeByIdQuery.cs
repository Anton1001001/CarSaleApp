namespace Car.Application.Queries.DriveType.GetDriveTypeById;

public record GetDriveTypeByIdQuery(int Id) : IRequest<GetDriveTypeByIdResponse>;