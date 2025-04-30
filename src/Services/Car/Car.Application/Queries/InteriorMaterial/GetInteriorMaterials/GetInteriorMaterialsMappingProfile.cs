namespace Car.Application.Queries.InteriorMaterial.GetInteriorMaterials;

public class GetInteriorMaterialsMappingProfile : Profile
{
    public GetInteriorMaterialsMappingProfile()
    {
        CreateMap<CarInteriorMaterial, GetInteriorMaterialsResponse>();
    }
}