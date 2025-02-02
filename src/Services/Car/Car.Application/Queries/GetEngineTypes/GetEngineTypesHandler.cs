namespace Car.Application.Queries.GetEngineTypes;

public class GetEngineTypesHandler(IRepository<CarEngineType> engineTypeRepository, IMapper mapper)
    : IRequestHandler<GetEngineTypesQuery, List<GetEngineTypesResponse>>
{
    public async Task<List<GetEngineTypesResponse>> Handle(GetEngineTypesQuery request,
        CancellationToken cancellationToken)
    {
        var engineTypes = await engineTypeRepository.GetAllAsync();
        var result = mapper.Map<List<GetEngineTypesResponse>>(engineTypes);
        return result;
    }
}