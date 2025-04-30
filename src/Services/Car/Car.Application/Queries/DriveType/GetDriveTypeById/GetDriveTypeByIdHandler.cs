namespace Car.Application.Queries.DriveType.GetDriveTypeById;

public class GetDriveTypeByIdHandler(IRepository<CarDriveType> driveTypeRepository) : IRequestHandler<GetDriveTypeByIdQuery, GetDriveTypeByIdResponse>
{
    public async Task<GetDriveTypeByIdResponse> Handle(GetDriveTypeByIdQuery request, CancellationToken cancellationToken)
    {
        var driveType = await driveTypeRepository.GetByIdAsync(request.Id, cancellationToken);
        return new GetDriveTypeByIdResponse(driveType.Id, driveType.Name);
    }
}