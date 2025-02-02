namespace Car.Application.Queries.GetInteriorMaterials;

public class GetInteriorMaterialsHandler(IRepository<CarInteriorMaterial> interiorMaterialRepository, IMapper mapper)
    : IRequestHandler<GetInteriorMaterialsQuery, List<GetInteriorMaterialsResponse>>
{
    public async Task<List<GetInteriorMaterialsResponse>> Handle(GetInteriorMaterialsQuery request,
        CancellationToken cancellationToken)
    {
        var interiorMaterials = await interiorMaterialRepository.GetAllAsync();
        var result = mapper.Map<List<GetInteriorMaterialsResponse>>(interiorMaterials);
        return result;
    }
}