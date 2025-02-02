namespace Car.Application.Queries.GetInteriorMaterials;

public class GetInteriorMaterialsMappingProfile : Profile
{
    public GetInteriorMaterialsMappingProfile()
    {
        CreateMap<CarInteriorMaterial, GetInteriorMaterialsResponse>();
    }
}