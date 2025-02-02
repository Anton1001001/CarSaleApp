namespace Car.Application.Queries.GetEngineTypes;

public class GetEngineTypesMappingProfile : Profile
{
    public GetEngineTypesMappingProfile()
    {
        CreateMap<CarEngineType, GetEngineTypesResponse>();
    }
}