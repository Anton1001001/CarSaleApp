namespace Car.Application.Queries.GetBodyTypes;

public class GetBodyTypesMappingProfile : Profile
{
    public GetBodyTypesMappingProfile()
    {
        CreateMap<CarBodyType, GetBodyTypesResponse>();
    }
}