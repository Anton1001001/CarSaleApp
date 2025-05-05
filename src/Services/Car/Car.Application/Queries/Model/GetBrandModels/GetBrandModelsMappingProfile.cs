namespace Car.Application.Queries.Model.GetBrandModels;

public class GetBrandModelsMappingProfile : Profile
{
    public GetBrandModelsMappingProfile()
    {
        CreateMap<CarModel, GetBrandModelsResponse>();
    }
}