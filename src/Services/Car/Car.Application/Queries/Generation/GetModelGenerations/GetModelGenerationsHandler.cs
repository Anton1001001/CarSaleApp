namespace Car.Application.Queries.Generation.GetModelGenerations;

public class GetModelGenerationsHandler(ICarModelRepository carModelRepository, IMapper mapper)
    : IRequestHandler<GetModelGenerationsQuery, List<GetModelGenerationsResponse>>
{
    public async Task<List<GetModelGenerationsResponse>> Handle(GetModelGenerationsQuery request,
        CancellationToken cancellationToken)
    {
        var generations = await carModelRepository.GetGenerationsAsync(request.ModelId, cancellationToken, request.Year);
        var response = mapper.Map<List<GetModelGenerationsResponse>>(generations);
        return response;
    }
}