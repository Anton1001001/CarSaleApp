namespace Car.Application.Queries.GetExchangeTypes;

public class GetExchangeTypesMappingProfile : Profile
{
    public GetExchangeTypesMappingProfile()
    {
        CreateMap<CarExchangeOption, GetExchangeTypesResponse>();
    }
}