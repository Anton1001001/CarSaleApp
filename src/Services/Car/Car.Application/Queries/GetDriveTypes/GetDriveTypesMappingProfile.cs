namespace Car.Application.Queries.GetDriveTypes;

public class GetDriveTypesMappingProfile : Profile
{
    public GetDriveTypesMappingProfile()
    {
        CreateMap<CarDriveType, GetDriveTypesResponse>();
    }
}