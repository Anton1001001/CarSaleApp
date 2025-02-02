namespace Car.Application.Queries.GetBrandModels;

public class GetBrandModelsMappingProfile : Profile
{
    public GetBrandModelsMappingProfile()
    {
        CreateMap<CarModel, GetBrandModelsResponse>();
    }
}