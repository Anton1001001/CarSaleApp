namespace Car.Application.Queries.Brand.GetBrands;

public class GetBrandsMappingProfile : Profile
{
    public GetBrandsMappingProfile()
    {
        CreateMap<CarBrand, GetBrandsResponse>();
    }
}