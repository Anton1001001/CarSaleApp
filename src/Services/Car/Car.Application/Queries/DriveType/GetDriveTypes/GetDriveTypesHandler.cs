namespace Car.Application.Queries.DriveType.GetDriveTypes;

public class GetDriveTypesHandler(IRepository<CarDriveType> driveTypeRepository, IMapper mapper) : IRequestHandler<GetDriveTypesQuery, List<GetDriveTypesResponse>>
{
    public async Task<List<GetDriveTypesResponse>> Handle(GetDriveTypesQuery request, CancellationToken cancellationToken)
    {
        var driveTypes = await driveTypeRepository.GetAllAsync();
        var result = mapper.Map<List<GetDriveTypesResponse>>(driveTypes);
        return result;
    }
}