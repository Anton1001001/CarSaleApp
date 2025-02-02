namespace Car.Application.Queries.GetExchangeTypes;

public class GetExchangeTypesHandler(IRepository<CarExchangeOption> exchangeTypeRepository, IMapper mapper)
    : IRequestHandler<GetExchangeTypesQuery, List<GetExchangeTypesResponse>>
{
    public async Task<List<GetExchangeTypesResponse>> Handle(GetExchangeTypesQuery request,
        CancellationToken cancellationToken)
    {
        var exchangeTypes = await exchangeTypeRepository.GetAllAsync();
        var result = mapper.Map<List<GetExchangeTypesResponse>>(exchangeTypes);
        return result;
    }
}