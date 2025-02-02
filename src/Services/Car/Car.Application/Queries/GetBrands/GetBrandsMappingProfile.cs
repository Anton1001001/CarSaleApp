namespace Car.Application.Queries.GetBrands;

public class GetBrandsMappingProfile : Profile
{
    public GetBrandsMappingProfile()
    {
        CreateMap<CarBrand, GetBrandsResponse>();
    }
}