namespace Car.Application.Queries.EngineType.GetEngineTypes;

public class GetEngineTypesMappingProfile : Profile
{
    public GetEngineTypesMappingProfile()
    {
        CreateMap<CarEngineType, GetEngineTypesResponse>();
    }
}