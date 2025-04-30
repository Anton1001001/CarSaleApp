namespace Car.Application.Queries.BodyType.GetBodyTypes;

public class GetBodyTypesMappingProfile : Profile
{
    public GetBodyTypesMappingProfile()
    {
        CreateMap<CarBodyType, GetBodyTypesResponse>();
    }
}