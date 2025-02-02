namespace Car.Application.Queries.GetConditions;

public class GetConditionsHandler(IRepository<CarCondition> conditionRepository, IMapper mapper)
    : IRequestHandler<GetConditionsQuery, List<GetConditionsResponse>>
{
    public async Task<List<GetConditionsResponse>> Handle(GetConditionsQuery request, CancellationToken cancellationToken)
    {
        var colors = await conditionRepository.GetAllAsync();
        var result = mapper.Map<List<GetConditionsResponse>>(colors);
        return result;
    }
}