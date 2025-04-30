namespace Car.Application.Queries.DriveType.GetDriveTypes;

public class GetDriveTypesMappingProfile : Profile
{
    public GetDriveTypesMappingProfile()
    {
        CreateMap<CarDriveType, GetDriveTypesResponse>();
    }
}